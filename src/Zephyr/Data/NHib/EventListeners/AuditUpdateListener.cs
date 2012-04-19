using System;
using System.Collections;
using System.Collections.Generic;
using NHibernate.Event;
using NHibernate.Event.Default;
using Zephyr.Data.Repository.Contract;
using Zephyr.Domain;
using Zephyr.Domain.Audit;
using System.Linq;

namespace Zephyr.Data.NHib.EventListeners
{

    /// <summary>
    /// Enables Soft Deletion by IsDeleted Flag from 
    /// </summary>
    internal class AuditUpdateListener : IPreUpdateEventListener
    {
        private readonly IRepository<AuditChangeLog> repository;

        public bool OnPreUpdate(PreUpdateEvent e)
        {
            if(e.Entity is Entity)
            {
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
                            
                            var changeLog = new AuditChangeLog();
                            changeLog.EntityType = e.Entity.GetType().Name;
                            changeLog.PropertyName = e.Persister.PropertyNames[dirtyFieldIndex];


                            changeLog.OldPropertyValue = GetPropertyValue(e.OldState[dirtyFieldIndex], changeLog.PropertyName, entity);
                            changeLog.NewPropertyValue = GetPropertyValue(e.State[dirtyFieldIndex], changeLog.PropertyName, entity);

                            changeLogs.Add(changeLog);
                        }

                        changeLogs.ForEach(log=>e.Session.SaveOrUpdate(log));
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

                return propertyState.ToString();
            }

            return null;
        }
    }
}