using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyFrameWork.NHib;
using NHibernate.Tool.hbm2ddl;

namespace MyTests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class DbTests
    {
        public DbTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void DropAndRecreateSchema()
        {
            //
            // TODO: Add test logic here
            //
            MyFrameWork.NHib.NHibernateSession.Initialize(null);

            var config = NHibernateSession.Configuration;

            var schemaExport = new SchemaExport(config);
            schemaExport.Execute(true, true, false);
        }

        [TestMethod]
        public void TryAlterDbSchema()
        {
            //
            // TODO: Add test logic here
            //
            MyFrameWork.NHib.NHibernateSession.Initialize(null);            

            var config = NHibernateSession.Configuration;

            var schemaUpdate = new SchemaUpdate(config);
            schemaUpdate.Execute(true, true);
        }        
    }
}
