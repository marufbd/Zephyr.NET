using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Zephyr.Configuration;
using Zephyr.Web.Mvc.Controllers;


namespace Zephyr.Web.Mvc.Windsor.Installers
{
    [InstallerPriority(0)]  // first to install as to register ZephyrConfiguration in prior
    public class ControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //register framework settings as singleton
            container.Register(
                Component.For<ZephyrConfiguration>().UsingFactoryMethod(_ => ZephyrConfiguration.ZephyrSettings).
                    LifestyleSingleton());

            var config = container.Resolve<ZephyrConfiguration>();
            foreach (var asmName in config.PersistenceConfig.MappingAssemblies)
            {
                container.Register(
                    Classes.FromAssembly(Assembly.Load(asmName)).BasedOn<ZephyrController>().LifestyleTransient());
            }            
        }
    }
}