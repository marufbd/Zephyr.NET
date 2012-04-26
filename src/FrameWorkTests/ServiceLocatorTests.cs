using DemoApp.Web.DomainModels;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using NUnit.Framework;
using Zephyr.Data.Repository;
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
            //// without Unit Of Work from PerWebRequest lifecycle
            //var r = ServiceLocator.Current.GetInstance<IRepository<Book>>();

            //Assert.NotNull(r);

            // with Unit Of Work
            using (UnitOfWorkScope.Start())
            {
                var repoBook = ServiceLocator.Current.GetInstance<IRepository<Book>>();
                var repoPub = ServiceLocator.Current.GetInstance<IRepository<Publisher>>();
                
                Assert.NotNull(repoBook);
                Assert.NotNull(repoPub);

                Assert.True(repoBook is NhRepository<Book>);
                Assert.True(repoPub is NhRepository<Publisher>);
                
                //see if two repos in same unit of work operates on same ISession, required for a
                // business transaction
                Assert.True( (((NhRepository<Book>) repoBook).Session==((NhRepository<Publisher>) repoPub).Session) );
            }             
        }        
    }
}
