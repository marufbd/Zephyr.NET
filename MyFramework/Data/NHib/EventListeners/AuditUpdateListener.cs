using System;
using NHibernate.Event;
using NHibernate.Event.Default;
using Zephyr.Domain;

namespace Zephyr.Data.NHib.EventListeners
{

    /// <summary>
    /// Enables Soft Deletion by IsDeleted Flag from 
    /// </summary>
    public class AuditUpdateListener : DefaultUpdateEventListener
    {
        protected override object PerformSaveOrUpdate(SaveOrUpdateEvent @event)
        {
            //need to implement the condition check
            if (@event.Entity is Entity)
            {
                ((Entity)@event.Entity).LastUpdatedAt = DateTime.UtcNow;
            }
            
             return base.PerformSaveOrUpdate(@event);
        }
    }
}