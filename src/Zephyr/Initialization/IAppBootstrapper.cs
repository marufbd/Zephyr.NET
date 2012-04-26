using Microsoft.Practices.ServiceLocation;

namespace Zephyr.Initialization
{
    public interface IAppBootstrapper
    { 
        void Run();

        void Dispose();
    }
}