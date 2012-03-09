using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Zephyr.Configuration;
using Zephyr.Data.Repository;
using Zephyr.Data.Repository.Contract;

namespace Zephyr.Web.Mvc.Windsor.Installers
{
    public class RepositoriesInstaller: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For(typeof(IRepository<>))
                        .ImplementedBy(typeof(NhRepository<>))
                        .LifestyleTransient());
        }
    }
}