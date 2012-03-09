using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Zephyr.Configuration;
using Zephyr.Web.Mvc.Controllers;


namespace Zephyr.Web.Mvc.Windsor.Installers
{
    public class ControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var config=new ZephyrConfig();
            foreach (var asmName in config.DataConfig.MappingAssemblies)
            {
                container.Register(
                    Classes.FromAssembly(Assembly.LoadFrom(asmName)).BasedOn<ZephyrController>().LifestyleTransient());
            }            
        }
    }
}