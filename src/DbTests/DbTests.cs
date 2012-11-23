using System;
using Microsoft.Practices.ServiceLocation;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Zephyr.Configuration;
using Zephyr.Initialization;
using Zephyr.Web.Mvc.Initialization;

namespace DbTests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestFixture]
    public class DbTests
    {
        private readonly IAppBootstrapper _appBootstrapper = new MvcAppBootstrapper();

        [SetUp]
        public void Init()
        {
            _appBootstrapper.Run();
        }

        [TearDown]
        public void EndCase()
        {
            _appBootstrapper.Dispose();
        }

        
        [Test]
        public void DropAndRecreateSchema()
        {
            var config = ServiceLocator.Current.GetInstance<NHibernate.Cfg.Configuration>();

            var zephyrConfig = ServiceLocator.Current.GetInstance<ZephyrConfiguration>();

            var schemaExport = new SchemaExport(config);
            if (zephyrConfig.PersistenceConfig.DbSchemaExportEnabled)
            {
                var path = zephyrConfig.PersistenceConfig.DbSchemaExportPath;
                schemaExport.SetOutputFile(path + "/schema_" + DateTime.Now.ToString("yyyy-MM-dd_HH mm ss") + ".sql");
            }                
            schemaExport.Execute(true, false, false);
        }

        [Test]
        public void TryAlterDbSchema()
        {
            var config = ServiceLocator.Current.GetInstance<NHibernate.Cfg.Configuration>();            
            
            var schemaUpdate = new SchemaUpdate(config);            
            schemaUpdate.Execute(true, true);
        }        
    }
}
