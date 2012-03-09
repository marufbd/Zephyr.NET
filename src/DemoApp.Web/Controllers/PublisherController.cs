using System;
using System.Web.Mvc;
using DemoApp.Web.DomainModels;
using Zephyr.Data.Repository;
using Zephyr.Data.Repository.Contract;
using Zephyr.Web.Mvc.Controllers;
using System.Linq;

namespace DemoApp.Web.Controllers
{
    public class PublisherController : ZephyrController
    {
        private readonly IRepository<Publisher> _repository;

        public PublisherController(IRepository<Publisher> repository)
        {
            _repository = repository;
        }

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

        [HttpPost]
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
