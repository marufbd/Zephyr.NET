using System;
using NHibernate;
using Zephyr.Data.NHib.UoW;
using Zephyr.DesignByContract;

namespace Zephyr.Data.UnitOfWork
{
    public static class UnitOfWorkScope
    {        
        private static IUnitOfWorkFactory _innerUnitOfWorkFactory; 
        private static IUnitOfWork _innerUnitOfWork; 

        public static IUnitOfWork Start()
        {
            _innerUnitOfWorkFactory = new NhUnitOfWorkFactory();

            _innerUnitOfWork = _innerUnitOfWorkFactory.Create();
            
            return Current;
        }

        public static bool IsStarted
        {
            get { return _innerUnitOfWork != null; }
        }        

        public static IUnitOfWork Current
        {
            get
            {
                if (_innerUnitOfWork == null)
                    throw new InvalidOperationException("You are not in a unit of work.");
                return _innerUnitOfWork;
            }
        }

        public static IUnitOfWorkFactory Factory
        {
            get
            {
                if (_innerUnitOfWorkFactory == null)
                    throw new InvalidOperationException("You are not in a unit of work.");

                return _innerUnitOfWorkFactory;
            }
        }

        public static void DisposeUnitOfWork()
        {
            _innerUnitOfWork = null;
            _innerUnitOfWorkFactory = null;
        }
    }    
}