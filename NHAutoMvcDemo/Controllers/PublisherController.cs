using System.Collections.Generic;
using System.Web.Mvc;
using MyFrameWork.Repository;
using MyFrameWork.Repository.Contract;
using NHAutoMvcDemo.DomainModels;

namespace NHAutoMvcDemo.Controllers
{
    public class PublisherController : Controller
    {
        private readonly IRepository<Publisher> _repository=new NhRepository<Publisher>();

        //
        // GET: /Publisher/

        public ActionResult Index()
        {
            var model = _repository.GetAll();
            
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new Publisher();

            return View("Edit", model);
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            Publisher publisher = _repository.Get(id);
            return View(publisher);
        }

        [HttpPost]
        public ActionResult Edit(Publisher publisher)
        {
            if(ModelState.IsValid)
            {
                _repository.SaveOrUpdate(publisher);
                return RedirectToAction("Index");
            }

            return View("Edit", publisher);
        }

        
        public ActionResult Delete(long id)
        {
            _repository.Delete(id);

            return RedirectToAction("Index");
        }

        public ActionResult Details(long id)
        {
            var publisher = _repository.Get(id);  

            return View(publisher);
        }

    }
}
