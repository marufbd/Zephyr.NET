namespace Zephyr.Initialization
{
    public interface IAppBootstrapper
    {
        void Run();

        void Dispose();
    }
}