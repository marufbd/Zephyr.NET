using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MyFrameWork.Repository;
using MyFrameWork.Repository.Contract;
using NHAutoMvcDemo.DomainModels;
using NHAutoMvcDemo.Models;

namespace NHAutoMvcDemo.Controllers
{
    public class StoreController : Controller
    {
        private readonly IRepository<Book> _repository = new NhRepository<Book>();
        //
        // GET: /Book/

        public ActionResult Index()
        {
            var model = _repository.GetAll() as IEnumerable<Book>;
            
            
            return View(model.OrderByDescending(b=>b.PublishedDate));
        }

        public ActionResult AddBook()
        {
            IRepository<Publisher> pubRepo=new NhRepository<Publisher>();            

            SelectList lstPublishers = new SelectList(pubRepo.GetAll(), "Id", "PublisherName");
            

            return View("SaveBook", new VmBook() { Book = new Book(), PublisherList = lstPublishers });
        }

        public ActionResult SaveBook(long id)
        {
            IRepository<Publisher> pubRepo = new NhRepository<Publisher>();
            var editBook = _repository.Get(id);

            var lstPublishers = new SelectList(pubRepo.GetAll(), "Id", "PublisherName");

            return View(new VmBook() { Book = editBook, PublisherList = lstPublishers, SelectPublisherId = editBook.Publisher.Id });
        }

        [HttpPost]
        public ActionResult SaveBook(VmBook vmbook)
        {
            if (ModelState.IsValid)
            {
                IRepository<Publisher> pubRepo = new NhRepository<Publisher>();
                vmbook.Book.Publisher = pubRepo.Get(vmbook.SelectPublisherId);
                _repository.SaveOrUpdate(vmbook.Book);
                return RedirectToAction("Index");
            }
            else
            {
                IRepository<Publisher> pubRepo = new NhRepository<Publisher>();                

                vmbook.PublisherList = new SelectList(pubRepo.GetAll(), "Id", "PublisherName");

                return View(vmbook);
            }            
        }

        public ActionResult BookDetails(long id)
        {
            return View(_repository.Get(id));
        }

        [HttpPost]
        public ActionResult DeleteBook(long id)
        {
            _repository.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
