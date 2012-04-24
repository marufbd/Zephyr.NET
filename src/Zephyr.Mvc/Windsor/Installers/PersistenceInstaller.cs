using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using NHibernate;
using Zephyr.Data.NHib;

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
            Kernel.Register(Component.For<ISessionFactory>().UsingFactoryMethod(_ => NHibernateSession.Factory).LifestyleSingleton(),
                            Component.For<ISession>().UsingFactoryMethod(k => k.Resolve<ISessionFactory>().OpenSession())
                                .LifestylePerWebRequest());
        }
    }
}