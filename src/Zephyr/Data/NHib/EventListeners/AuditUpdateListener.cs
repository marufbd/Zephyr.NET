using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using NHibernate;
using NHibernate.Event;
using Zephyr.Domain;
using Zephyr.Domain.Audit;
using Zephyr.Exceptions;

namespace Zephyr.Data.NHib.EventListeners
{

    /// <summary>
    /// Enables Soft Deletion by IsDeleted Flag from 
    /// </summary>
    internal class AuditUpdateListener : IPreUpdateEventListener
    { 
        public bool OnPreUpdate(PreUpdateEvent e)
        {
            if(e.Entity is Entity)
            {
                if(e.OldState==null)
                    throw new AuditException();

                var dirtyFieldIndexes = e.Persister.FindDirty(e.State, e.OldState, e.Entity, e.Session);

                if(dirtyFieldIndexes.Length>0)
                {
                    var entity = e.Entity as Entity;
                    entity.LastUpdatedAt = DateTime.UtcNow;

                    var changeLogs = new List<AuditChangeLog>();
                    if (e.Entity.GetType().GetInterface(typeof(IAuditable).Name) != null)
                    {
                        foreach (var dirtyFieldIndex in dirtyFieldIndexes)
                        {
                            if (e.OldState[dirtyFieldIndex] == e.State[dirtyFieldIndex]) continue;
                            
                            //insert audit changelog here
                            var changeLog = new AuditChangeLog
                                                {
                                                    EntityType = e.Entity.GetType().Name,
                                                    EntityIdentifier = entity.Id.ToString(CultureInfo.InvariantCulture),
                                                    ActionType = AuditType.Update,
                                                    PropertyName = e.Persister.PropertyNames[dirtyFieldIndex]
                                                };

                            changeLog.OldPropertyValue = GetPropertyValue(e.OldState[dirtyFieldIndex], changeLog.PropertyName, entity);
                            changeLog.NewPropertyValue = GetPropertyValue(e.State[dirtyFieldIndex], changeLog.PropertyName, entity);

                            changeLogs.Add(changeLog); 
                        }

                        IStatelessSession session = e.Session.SessionFactory.OpenStatelessSession();
                        changeLogs.ForEach(log=>session.Insert(log));
                        session.Close();
                    } 
                }

            }

            return false;
        }


        private string GetPropertyValue(object propertyState, string propertyName, Entity entity)
        {
            if (propertyState != null)
            {
                //Type entityType = entity.GetType();
                Type propertyType = propertyState.GetType();
                if (propertyType == typeof(IList<>) || propertyType == typeof(IList) ||
                    propertyType == typeof(IEnumerable<>) || propertyType == typeof(IEnumerable))
                {

                }
                else if(propertyType.IsAssignableFrom(typeof(Entity)))
                {
                    
                }

                return propertyState.ToString();
            }

            return null;
        }
    }
}