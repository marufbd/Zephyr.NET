using System;
using Zephyr.DesignByContract;

namespace Zephyr.Data.UnitOfWork
{
    public static class UnitOfWork
    {
        private static IUnitOfWorkFactory _unitOfWorkFactory;
        private static IUnitOfWork _innerUnitOfWork;

        public static IUnitOfWork Start()
        {
            _innerUnitOfWork = _unitOfWorkFactory.Create();
            return _innerUnitOfWork;
        }

        public static IUnitOfWork Current { get; private set; }
    }    
}