using System;
using NHibernate;
using Zephyr.Data.UnitOfWork;

namespace Zephyr.Data.NHib.UoW
{
    internal sealed class NhUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private ISession _currentSession;
        private ISessionFactory _sessionFactory;

        public NHibernate.Cfg.Configuration Configuration { get; private set; }
        public ISession CurrentSession { 
            get
            { 
                if(_currentSession==null)
                    throw new InvalidOperationException("There is no Unit of Work open.");

                return _currentSession;
            } 
        } 
        
        public IUnitOfWork Create()
        {
            _currentSession = NHibernateSession.Initialize();
            Configuration = NHibernateSession.Configuration;
            _sessionFactory = NHibernateSession.Factory;

            return new NhUnitOfWork(this, _currentSession);
        }

        public void DisposeUnitOfWork()
        {
            _currentSession = null;
        }
    }
}