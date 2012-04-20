using Zephyr.Data.UnitOfWork;

namespace Zephyr.Data.NHib.UoW
{
    internal sealed class NhUnitOfWorkfactory : IUnitOfWorkFactory
    {

        
        public IUnitOfWork Create()
        {
            return new NhUnitOfWork(NHibernateSession.Initialize());
        }
    }
}