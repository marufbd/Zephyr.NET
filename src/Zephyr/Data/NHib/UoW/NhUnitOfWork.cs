using NHibernate;
using Zephyr.Data.UnitOfWork;

namespace Zephyr.Data.NHib.UoW
{
    internal sealed class NhUnitOfWork : IUnitOfWork
    {
        private ISession _session;
        private IUnitOfWorkFactory _factory;

        public NhUnitOfWork(ISession session)
        {
            _session = session;
        }

        public ISession CurrentSession { get { return _session; } }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}