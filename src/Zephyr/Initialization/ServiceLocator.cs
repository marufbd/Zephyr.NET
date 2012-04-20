using Castle.Windsor;
using Castle.Windsor.Installer;
using CommonServiceLocator.WindsorAdapter;
using Microsoft.Practices.ServiceLocation;

namespace Zephyr.Initialization
{
    public static class ServiceLocator
    {
        public static void Initialize()
        {
            var container = new WindsorContainer().Install(FromAssembly.Containing<IAppBootstrapper>());

            Current = new WindsorServiceLocator(container);
        }

        public static IServiceLocator Current { get; private set; }
    }
}