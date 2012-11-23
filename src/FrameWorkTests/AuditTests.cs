using System;
using System.Collections.Generic;
using DemoApp.Web.DomainModels;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using NHibernate.Envers;
using NHibernate.Envers.Query;
using NUnit.Framework;
using Zephyr.Data.NHib;
using Zephyr.Data.Repository;
using Zephyr.Data.Repository.Contract;
using Zephyr.Data.UnitOfWork;
using Zephyr.Domain.Audit;
using Zephyr.Initialization;
using Zephyr.Web.Mvc.Initialization;
using System.Linq;

namespace FrameWorkTests {
    [TestFixture]
    public class AuditTests
    {
        private IAppBootstrapper _appBootstrapper=new MvcAppBootstrapper();

        [SetUp]
        public void Init()
        {
            //ZephyrContext.IsTestMode = true;
            _appBootstrapper.Run();

            //using(UnitOfWorkScope.Start())
            //{
            //    var repoPub = new NhRepository<Publisher>();
            //    var repoBook = new NhRepository<Book>();

            //    //add publisher
            //    var pub1=new Publisher(){PublisherName = "AuditTest - New Pub 1"};
            //    var pub2 = new Publisher() { PublisherName = "AuditTest - New Pub 2" };
            //    repoPub.SaveOrUpdate(pub1);
            //    repoPub.SaveOrUpdate(pub2);

            //    //add new book
            //    var book = new Book
            //                   {
            //                       BookName = "AuditTest - New Book 1",
            //                       BookDescription = "AuditTest - Sample book description book 1",
            //                       Publisher = pub1
            //                   };                

            //    //update book
            //    book.Publisher = pub2;
            //    repoBook.SaveOrUpdate(book);
            //}            
        }

        [TearDown]
        public void EndCase()
        {
            //using (UnitOfWorkScope.Start())
            //{
            //    var repoPub = new NhRepository<Publisher>();

            //    //clean up publisher , books will be auto deleted on cascade delete
            //    repoPub.Delete(repoPub.Query(e => e.PublisherName.Equals("AuditTest - New Pub 1")).FirstOrDefault());
            //    repoPub.Delete(repoPub.Query(e => e.PublisherName.Equals("AuditTest - New Pub 2")).FirstOrDefault());
            //}

            _appBootstrapper.Dispose();
        }

        private void PrintEntities(IEnumerable<Publisher> entities)
        {
            foreach (var publisher in entities)
            {                
                string str = String.Format("Guid: {0}, Name: {1}", publisher.Id, publisher.PublisherName);
                Console.WriteLine(str);
            }
        }

        private void PrintHistory(IEnumerable<IRevisionEntityInfo<Publisher, RevisionEntity>> revs)
        {
            foreach (var rev in revs)
            {
                string str =
                    String.Format(
                        "Operation: {0}, Revision By: {1}, TimeStamp:{2} #### Guid: {3}, Name: {4}, Book Count: {5}",
                        rev.Operation, rev.RevisionEntity.RevisionBy, rev.RevisionEntity.RevisionTimestamp,
                        rev.Entity.Id, rev.Entity.PublisherName, rev.Entity.Books.Count);
                
                Console.WriteLine(str);
            }
        }

        [Test]
        public void NEnversQuery()
        {
            using (var session=ServiceLocator.Current.GetInstance<ISession>())
            { 
                IAuditReader auditReader = session.Auditer();
                //var revEntity = auditReader.FindRevisions<Publisher>(auditReader.GetRevisions<Publisher>());

                //var rev = auditReader.CreateQuery().ForEntitiesAtRevision<Publisher>(1).Results();
                //Console.WriteLine("=======ForEntitiesAtRevision===========");
                //PrintEntities(rev);

                Console.WriteLine("=======ForHistoryOf===========");
                var history =
                    auditReader.CreateQuery().ForHistoryOf<Publisher, RevisionEntity>(true).Add(
                        AuditEntity.Property("PublisherName").Eq("Manning")).Results();

                PrintHistory(history);
            }             
        }        
    }
}
