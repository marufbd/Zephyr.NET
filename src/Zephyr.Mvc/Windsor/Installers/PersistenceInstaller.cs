using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using NHibernate;
using Zephyr.Data.NHib;
using Zephyr.Data.NHib.UoW;
using Zephyr.Data.UnitOfWork;

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
            NHibernateSession.Initialize();
            Kernel.Register(
                Component.For<ISessionFactory>().UsingFactoryMethod(_ => NHibernateSession.Factory).LifestyleSingleton(),
                            Component.For<ISession>().UsingFactoryMethod(k => k.Resolve<ISessionFactory>().OpenSession())
                                .LifestylePerWebRequest());


            Kernel.Register(Component.For<IUnitOfWorkFactory>().ImplementedBy<NhUnitOfWorkFactory>().LifestyleTransient());
            Kernel.Register(Component.For<IUnitOfWork>().UsingFactoryMethod(_=>UnitOfWorkScope.Current).LifestyleTransient());
        }
    }
}