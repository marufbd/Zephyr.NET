using Iesi.Collections;
using NHibernate.Engine;
using NHibernate.Event;
using NHibernate.Event.Default;
using NHibernate.Persister.Entity;
using Zephyr.Domain;

namespace Zephyr.Data.NHib.EventListeners
{

    /// <summary>
    /// Enables Soft Deletion by IsDeleted Flag from 
    /// </summary>
    public class SoftDeleteListener : DefaultDeleteEventListener
    {
        protected override void DeleteEntity(IEventSource session, object entity, EntityEntry entityEntry, bool isCascadeDeleteEnabled, IEntityPersister persister, ISet transientEntities)
        {
            //need to implement the condition check
            if (entity is Entity)
            {
                ((Entity) entity).IsDeleted = true;
                this.CascadeBeforeDelete(session, persister, entity, entityEntry, transientEntities);
                this.CascadeAfterDelete(session, persister, entity, transientEntities);                
            }
            else
            {
                base.DeleteEntity(session, entity, entityEntry, isCascadeDeleteEnabled, persister, transientEntities);    
            }
        }
    }
}