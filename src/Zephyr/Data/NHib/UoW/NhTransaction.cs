using NHibernate;
using Zephyr.Data.UnitOfWork;

namespace Zephyr.Data.NHib.UoW
{
    public class NhTransaction : IGenericTransaction
    {
        private readonly ITransaction _transaction;

        public NhTransaction(ITransaction transaction)
        {
            _transaction = transaction;
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public void Dispose()
        {
            _transaction.Dispose();
        }
    }
}