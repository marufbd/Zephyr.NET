namespace Zephyr.Data.UnitOfWork
{
    internal interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}