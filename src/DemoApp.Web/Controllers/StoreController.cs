using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DemoApp.Web.DomainModels;
using DemoApp.Web.ViewModels;
using Microsoft.Practices.ServiceLocation;
using Zephyr.Data.Models;
using Zephyr.Data.Repository;
using Zephyr.Data.Repository.Contract;
using Zephyr.Data.UnitOfWork;
using Zephyr.Domain.Audit;
using Zephyr.Web.Mvc.Controllers;

namespace DemoApp.Web.Controllers
{    
    public class StoreController : ZephyrController
    {
        private readonly IRepository<Book> _repositoryBook;
        private readonly IRepository<Publisher> _repositoryPublisher;

        public StoreController(IRepository<Book> repBook, IRepository<Publisher> repPub)
        {
            _repositoryBook = repBook;
            _repositoryPublisher = repPub;
        }

        //
        // GET: /Book/
        public ActionResult Index()
        {
            //var model = _repositoryBook.GetAllPaged(2, 2);
            var model = _repositoryBook.GetAll();

            return View(model);
        }

        public ActionResult AddBook()
        {
            SelectList lstPublishers = new SelectList(_repositoryPublisher.GetAll(), "Guid", "PublisherName");
            

            return View("SaveBook", new VmBook() { Book = new Book(), PublisherList = lstPublishers });
        }

        public ActionResult SaveBook(Guid guid)
        {            
            var editBook = _repositoryBook.Get(guid);

            var lstPublishers = new SelectList(_repositoryPublisher.GetAll(), "Guid", "PublisherName");

            return View(new VmBook() { Book = editBook, PublisherList = lstPublishers, SelectPublisherId = editBook.Publisher.Guid });
        }

        [HttpPost]
        public ActionResult SaveBook(VmBook vmbook)
        {             
            if (ModelState.IsValid && vmbook.Book.IsValid())
            {
                //always use Unit of work for save/update
                using (UnitOfWorkScope.Start())
                {
                    var repo = ServiceLocator.Current.GetInstance<IRepository<Book>>();
                    vmbook.Book.Publisher = _repositoryPublisher.Get(vmbook.SelectPublisherId);

                    //testing a business transaction
                    var repoAudit = ServiceLocator.Current.GetInstance<IRepository<AuditChangeLog>>();
                    var audit = new AuditChangeLog();
                    audit.ActionBy = "maruf";
                    audit.ActionType=AuditType.Update;
                    audit.OldPropertyValue = "Old val";
                    audit.NewPropertyValue = "New val";
                    
                    repoAudit.SaveOrUpdate(audit);
                    repo.SaveOrUpdate(vmbook.Book);
                }
                
                return RedirectToAction("Index");
            }
            else
            {
                vmbook.PublisherList = new SelectList(_repositoryPublisher.GetAll(), "Guid", "PublisherName");

                return View(vmbook);
            }            
        }

        public ActionResult BookDetails(Guid guid)
        {
            return View(_repositoryBook.Get(guid));
        }

        [HttpPost]
        public ActionResult DeleteBook(Guid guid)
        {
            using (UnitOfWorkScope.Start())
            {
                var repo = ServiceLocator.Current.GetInstance<IRepository<Book>>();
                repo.Delete(guid);
            }            

            return RedirectToAction("Index");
        }
    }
}
