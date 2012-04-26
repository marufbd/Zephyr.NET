namespace Zephyr.Data.UnitOfWork
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();

        void DisposeUnitOfWork();
    }
}