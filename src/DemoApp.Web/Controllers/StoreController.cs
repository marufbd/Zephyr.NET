using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DemoApp.Web.DomainModels;
using DemoApp.Web.ViewModels;
using Zephyr.Data.Models;
using Zephyr.Data.Repository;
using Zephyr.Data.Repository.Contract;
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
            SelectList lstPublishers = new SelectList(_repositoryPublisher.GetAll(), "Id", "PublisherName");
            

            return View("SaveBook", new VmBook() { Book = new Book(), PublisherList = lstPublishers });
        }

        public ActionResult SaveBook(long id)
        {            
            var editBook = _repositoryBook.Get(id);

            var lstPublishers = new SelectList(_repositoryPublisher.GetAll(), "Id", "PublisherName");

            return View(new VmBook() { Book = editBook, PublisherList = lstPublishers, SelectPublisherId = editBook.Publisher.Id });
        }

        [HttpPost]
        public ActionResult SaveBook(VmBook vmbook)
        {            
            vmbook.Book = _repositoryBook.Get(vmbook.Book.Id);

            if (TryUpdateModel(vmbook) &&  ModelState.IsValid)
            {
                vmbook.Book.Publisher = _repositoryPublisher.Get(vmbook.SelectPublisherId);
                
                _repositoryBook.SaveOrUpdate(vmbook.Book);
                
                return RedirectToAction("Index");
            }
            else
            {
                vmbook.PublisherList = new SelectList(_repositoryPublisher.GetAll(), "Id", "PublisherName");

                return View(vmbook);
            }            
        }

        public ActionResult BookDetails(long id)
        {
            return View(_repositoryBook.Get(id));
        }

        [HttpPost]
        public ActionResult DeleteBook(long id)
        {
            _repositoryBook.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
