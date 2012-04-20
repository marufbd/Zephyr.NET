using NHibernate;
using Zephyr.Data.UnitOfWork;

namespace Zephyr.Data.NHib.UoW
{
    internal sealed class NhUnitOfWork : IUnitOfWork
    {
        private ISession _session;

        public NhUnitOfWork(ISession session)
        {
            _session = session;
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}