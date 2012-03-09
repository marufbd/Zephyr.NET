using System;
using NHibernate.Event;
using NHibernate.Event.Default;
using Zephyr.Domain;

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
                var dirtyFieldIndexes = e.Persister.FindDirty(e.State, e.OldState, e.Entity, e.Session);
                foreach (var dirtyFieldIndex in dirtyFieldIndexes)
                {
                    if(e.OldState[dirtyFieldIndex]!=e.State[dirtyFieldIndex])
                    {
                        var entity = e.Entity as Entity;
                        entity.LastUpdatedAt = DateTime.UtcNow;
                    }
                }
            }

            return false;
        }
    }
}