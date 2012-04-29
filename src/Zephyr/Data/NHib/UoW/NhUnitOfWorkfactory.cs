using System;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using Zephyr.Data.UnitOfWork;

namespace Zephyr.Data.NHib.UoW
{
    public class NhUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private ISession _currentSession;
        //private ISessionFactory _sessionFactory; 
        
        public IUnitOfWork Create()
        {            
            //_sessionFactory = ServiceLocator.Current.GetInstance<ISessionFactory>();
            //_currentSession = _sessionFactory.OpenSession();
            _currentSession = ServiceLocator.Current.GetInstance<ISession>();

            return new NhUnitOfWork(this, _currentSession);
        }

        public void DisposeUnitOfWork()
        {
            _currentSession = null;
        }
    }
}