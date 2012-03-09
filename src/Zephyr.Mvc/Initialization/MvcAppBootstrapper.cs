using System.Web.Mvc;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Zephyr.Initialization;
using Zephyr.Web.Mvc.Windsor;

namespace Zephyr.Web.Mvc.Initialization
{
    public class MvcAppBootstrapper : IAppBootstrapper
    {
        private static IWindsorContainer _container;

        public void Run()
        {
            _container = new WindsorContainer().Install(FromAssembly.Containing<WindsorControllerFactory>());
            var controllerFactory = new WindsorControllerFactory(_container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}