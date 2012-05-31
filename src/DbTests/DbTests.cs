using System;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Zephyr.Configuration;
using Zephyr.Data.NHib;

namespace MyTests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestFixture]
    public class DbTests
    {                

        [Test]
        public void DropAndRecreateSchema()
        {
            //
            // TODO: Add test logic here
            //
            NHibernateSession.Initialize(null);

            var config = NHibernateSession.Configuration;
            var zephyrConfig = new ZephyrConfig();

            var schemaExport = new SchemaExport(config);
            if (zephyrConfig.ExportDbSchema)
            {
                var path = zephyrConfig.DataConfig.DbSchemaExportPath;
                schemaExport.SetOutputFile(path + "/schema_" + DateTime.Now.ToString("yyyy-MM-dd_HH mm ss") + ".sql");
            }                
            schemaExport.Execute(true, false, false);
        }

        [Test]
        public void TryAlterDbSchema()
        {
            //
            // TODO: Add test logic here
            //
            NHibernateSession.Initialize(null);

            var config = NHibernateSession.Configuration;
            
            var schemaUpdate = new SchemaUpdate(config);            
            schemaUpdate.Execute(true, true);
        }        
    }
}
