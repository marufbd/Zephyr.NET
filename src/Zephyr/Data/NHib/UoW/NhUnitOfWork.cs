using System;
using System.Data;
using NHibernate;
using Zephyr.Data.UnitOfWork;

namespace Zephyr.Data.NHib.UoW
{
    internal sealed class NhUnitOfWork : IUnitOfWork
    {
        private readonly IUnitOfWorkFactory _factory;
        private ISession _session;
        private NhTransaction _transaction;

        public NhUnitOfWork(IUnitOfWorkFactory factory, ISession session)
        {
            _factory = factory;
            
            session.FlushMode = FlushMode.Commit;
            _session = session;

            BeginTransaction();
        }

        public ISession CurrentSession
        {
            get
            {
                if (_session == null)
                    throw new InvalidOperationException("There is no Unit of Work open.");

                return _session;
            }
        } 

        public bool IsInActiveTransaction {get { return _session.Transaction.IsActive; }}
        public void Flush()
        {
            _session.Flush();
        }

        public IGenericTransaction BeginTransaction()
        {
            _transaction=new NhTransaction(_session.BeginTransaction());

            return _transaction;
        }

        private IGenericTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            _transaction = new NhTransaction(_session.BeginTransaction(isolationLevel));

            return _transaction;
        }

        public void TransactionalFlush()
        {
            TransactionalFlush(IsolationLevel.ReadCommitted);
        }

        private void TransactionalFlush(IsolationLevel isolationLevel)
        {
            if(!IsInActiveTransaction)
                BeginTransaction(IsolationLevel.ReadCommitted);
            
            try
            {
                _transaction.Commit();
            }
            catch (Exception)
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
            }
        }

        public void Dispose()
        {
            if(_disposed)
                return;

            TransactionalFlush();

            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
            //if (_session != null)
            //{
            //    _session.Dispose();           
            //}
            _session = null;

            _factory.DisposeUnitOfWork();
            UnitOfWorkScope.DisposeUnitOfWork();

            _disposed = true;
        }

        private bool _disposed = false;
    }
}