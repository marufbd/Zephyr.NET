using NHibernate.Envers;
using Zephyr.Domain.Audit;
using Zephyr.Initialization;

namespace Zephyr.Data.NHib.EventListeners
{
    public class NEnversRevInfoListener : IRevisionListener
    { 
        public void NewRevision(object revisionEntity)
        {
            ((RevisionEntity) revisionEntity).RevisionBy = ZephyrContext.User.Identity.Name;
        }
    }

}