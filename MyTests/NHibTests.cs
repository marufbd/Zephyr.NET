using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyFrameWork.NHib;
using NHAutoMvcDemo.DomainModels;
using NHibernate;

namespace MyTests
{
    [TestClass]
    public class NHibTests
    {
        [TestMethod]
        public void LazyLoad()
        {
            ISession session = NHibernateSession.Initialize(null);

            var pub = session.Get<Publisher>(3L);

            Assert.IsFalse(NHibernateUtil.IsInitialized(pub.Books));
        }

        [TestMethod]
        public void IdentityScope()
        {
            ISession session = NHibernateSession.Initialize(null);
            
            var pub1 = session.Get<Publisher>(3L);
            session.Clear();
            session = NHibernateSession.Initialize(null);
            var pub2 = session.Get<Publisher>(3L);

            Assert.IsTrue(pub1==pub2);
            Assert.IsTrue(pub1.Equals(pub2));
        }


        [TestMethod]
        public void NPlus1()
        {
            ISession session = NHibernateSession.Initialize(null);

            var pubs = session.CreateCriteria<Publisher>().List<Publisher>();
            int totalBooks = 0;
            foreach (var publisher in pubs)
            {
                foreach (var book in publisher.Books)
                {
                    string name = book.BookName;
                    totalBooks += 1;
                }
            }

            Assert.AreEqual(4, totalBooks);
        }
    }
}
