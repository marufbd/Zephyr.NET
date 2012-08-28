using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using NHibernate;
using Zephyr.Configuration;
using Zephyr.Data.NHib;
using Zephyr.Data.NHib.UoW;
using Zephyr.Data.UnitOfWork;
using Zephyr.Initialization;

namespace Zephyr.Web.Mvc.Windsor.Installers
{
    public class PersistenceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<Persistencefacility>();
        }
    }

    public class Persistencefacility : AbstractFacility
    {
        protected override void Init()
        {
            //register framework settings as singleton
            Kernel.Register(Component.For<ZephyrConfiguration>().UsingFactoryMethod(_ => ZephyrConfiguration.ZephyrSettings).LifestyleSingleton()); 

            Kernel.Register(
                Component.For<ISessionFactory>().UsingFactoryMethod(
                    _ => NHibernateSession.Initialize(null, Kernel.Resolve<ZephyrConfiguration>())).LifestyleSingleton());

            if(ZephyrContext.IsTestMode)
            {
                Kernel.Register(
                Component.For<ISession>().UsingFactoryMethod(k => k.Resolve<ISessionFactory>().OpenSession())
                    .LifestyleTransient());
            }
            else
            {
                Kernel.Register(
                Component.For<ISession>().UsingFactoryMethod(k => k.Resolve<ISessionFactory>().OpenSession())
                    .LifestylePerWebRequest());
            }

            Kernel.Register(
                Component.For<IUnitOfWorkFactory>().ImplementedBy<NhUnitOfWorkFactory>().LifestyleTransient());
                                   

            //Kernel.Register(Component.For<IUnitOfWork>().UsingFactoryMethod(_=>UnitOfWorkScope.Current).LifestyleTransient());
        }
    }
}