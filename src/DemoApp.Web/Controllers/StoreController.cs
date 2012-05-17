using System;
using System.Web.Mvc;
using DemoApp.Web.DomainModels;
using DemoApp.Web.ViewModels;
using Microsoft.Practices.ServiceLocation;
using Zephyr.Data.Repository.Contract;
using Zephyr.Data.UnitOfWork;
using Zephyr.Specification;
using Zephyr.Web.Mvc.Controllers;
using Zephyr.Web.Mvc.ViewModels;
using Zephyr.Extensions;

namespace DemoApp.Web.Controllers
{
    public class StoreController : ZephyrCRUDController<Book>
    {
        private readonly IRepository<Book> _repositoryBook;
        private readonly IRepository<Publisher> _repositoryPublisher;

        public StoreController(IRepository<Book> repBook, IRepository<Publisher> repPub) : base(repBook)
        {
            _repositoryBook = repBook;
            _repositoryPublisher = repPub;
        }

        //
        // GET: /Book/
        public ActionResult Index()
        {                        
            return View();
        }

        public ActionResult Filter()
        {
            //var model = _repositoryBook.GetAllPaged(2, 2);
            //var model = _repositoryBook.Query(m => m.Publisher.PublisherName=="Manning");
            var querySpec=new Spec<Book>(m => m.Publisher.PublisherName == "Manning").And(new Spec<Book>(m => m.BookName.StartsWith("Clojure")));
            var model = _repositoryBook.Query(querySpec).ToPagedList(1, 5); //using specification
            //var model = _repositoryBook.GetAll();


            return View("List", new ListViewModel<Book>() {Model = model});
        }

        public ActionResult AddBook()
        {
            SelectList lstPublishers = new SelectList(_repositoryPublisher.GetAll(), "Id", "PublisherName");                       

            return View("SaveBook", new VmBook() { Book = new Book(), PublisherList = lstPublishers });
        }

        public ActionResult SaveBook(Guid guid)
        {            
            var editBook = _repositoryBook.Get(guid);

            var lstPublishers = new SelectList(_repositoryPublisher.GetAll(), "Id", "PublisherName");

            return View(new VmBook() { Book = editBook, PublisherList = lstPublishers, SelectPublisherId = editBook.Publisher.Id });
        }

        [HttpPost]
        public ActionResult SaveBook(VmBook vmbook)
        {            
            if (ModelState.IsValid)
            {
                //always use Unit of work for save/update
                using (UnitOfWorkScope.Start())
                {
                    var repoBook = ServiceLocator.Current.GetInstance<IRepository<Book>>();
                    var repoPub = ServiceLocator.Current.GetInstance<IRepository<Publisher>>();
                    //vmbook.Book = repo.Get(vmbook.Book.Id);
                    vmbook.Book.Publisher = repoPub.Get(vmbook.SelectPublisherId);

                    repoBook.SaveOrUpdate(vmbook.Book);
                    ////testing a business transaction
                    //var repoAudit = ServiceLocator.Current.GetInstance<IRepository<AuditChangeLog>>();
                    //var audit = new AuditChangeLog();
                    //audit.ActionBy = "maruf";
                    //audit.ActionType=AuditType.Update;
                    //audit.OldPropertyValue = "Old val";
                    //audit.NewPropertyValue = "New val";
                    //repoAudit.SaveOrUpdate(audit);
                    TempData.Add("Message", "New Book added successfully!");
                }
                
                return RedirectToAction("List");
            }
            else
            { 
                vmbook.PublisherList = new SelectList(_repositoryPublisher.GetAll(), "Id", "PublisherName");

                return View(vmbook);
            }            
        }
    }
}
