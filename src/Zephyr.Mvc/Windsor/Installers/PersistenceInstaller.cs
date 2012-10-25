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
            var windsorContainer = container;
            
            //NHibernate Configuration
            windsorContainer.Register(
                Component.For<NHibernate.Cfg.Configuration>().UsingFactoryMethod(
                    _ => NHibernateSession.Configure(null, windsorContainer.Resolve<ZephyrConfiguration>())).LifestyleSingleton());
            
            //Nhibernate session factory
            windsorContainer.Register(
                Component.For<ISessionFactory>().UsingFactoryMethod(
                    k => k.Resolve<NHibernate.Cfg.Configuration>().BuildSessionFactory()).LifestyleSingleton());            

            if (ZephyrContext.IsWebApplication)
            {
                windsorContainer.Register(
                Component.For<ISession>().UsingFactoryMethod(k => k.Resolve<ISessionFactory>().OpenSession())
                    .LifestylePerWebRequest());                
            }
            else
            {
                windsorContainer.Register(
                Component.For<ISession>().UsingFactoryMethod(k => k.Resolve<ISessionFactory>().OpenSession())
                    .LifestyleTransient());
            }

            


            windsorContainer.Register(
                Component.For<IUnitOfWorkFactory>().ImplementedBy<NhUnitOfWorkFactory>().LifestyleTransient());
        }
    }    
}