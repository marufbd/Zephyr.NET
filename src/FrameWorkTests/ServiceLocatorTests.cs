using DemoApp.Web.DomainModels;
using Microsoft.Practices.ServiceLocation;
using NUnit.Framework;
using Zephyr.Data.Repository.Contract;
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
        public void RepositoryInstanceGet()
        {
            var repo = ServiceLocator.Current.GetInstance<IRepository<Book>>();

            Assert.NotNull(repo);
        }

        
    }
}
