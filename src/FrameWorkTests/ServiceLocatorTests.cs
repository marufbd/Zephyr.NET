using DemoApp.Web.DomainModels;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using NUnit.Framework;
using Zephyr.Data.Repository.Contract;
using Zephyr.Data.UnitOfWork;
using Zephyr.Initialization;
using Zephyr.Web.Mvc.Initialization;

namespace FrameWorkTests {
    [TestFixture]
    public class ServiceLocatorTests
    {
        private IAppBootstrapper appBootstrapper;

        [SetUp]
        public void Init()
        {
            appBootstrapper = new MvcAppBootstrapper();
            appBootstrapper.Run();
        }

        [TearDown]
        public void EndCase()
        {
            appBootstrapper.Dispose();
        }


        [Test]
        public void IsessionFactoryGet()
        {
            var sessionFactory = ServiceLocator.Current.GetInstance<ISessionFactory>();

            Assert.NotNull(sessionFactory);
        }     
        
        [Test]
        public void RepositoryInstanceGet()
        {
            // without Unit Of Work
            var r = ServiceLocator.Current.GetInstance<IRepository<Book>>();

            Assert.NotNull(r);

            // with Unit Of Work
            using (UnitOfWorkScope.Start())
            {
                var repo = ServiceLocator.Current.GetInstance<IRepository<Book>>();
                
                Assert.NotNull(repo);
            }             
        }        
    }
}
