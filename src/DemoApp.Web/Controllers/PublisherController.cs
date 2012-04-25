using System;
using System.Web.Mvc;
using DemoApp.Web.DomainModels;
using Microsoft.Practices.ServiceLocation;
using Zephyr.Data.Repository;
using Zephyr.Data.Repository.Contract;
using Zephyr.Data.UnitOfWork;
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
            using (UnitOfWorkScope.Start())
            {
                var repo = ServiceLocator.Current.GetInstance<IRepository<Publisher>>();
                var pub = repo.Get(publisher.Id);

                if (TryUpdateModel(pub) && ModelState.IsValid)
                {
                    repo.SaveOrUpdate(pub);
                    return RedirectToAction("Index");
                }

                return View("Edit", publisher);
            } 
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
