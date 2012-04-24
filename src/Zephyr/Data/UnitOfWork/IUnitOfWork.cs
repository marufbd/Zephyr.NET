using System;

namespace Zephyr.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericTransaction BeginTransaction();
        void TransactionalFlush();
    }
}