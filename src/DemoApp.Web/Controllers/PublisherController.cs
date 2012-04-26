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
            if(ModelState.IsValid)
            {
                //always use Unit of work for save/update
                using (UnitOfWorkScope.Start())
                {
                    var repo = ServiceLocator.Current.GetInstance<IRepository<Publisher>>();

                    if (TryUpdateModel(publisher) && ModelState.IsValid)
                    {
                        repo.SaveOrUpdate(publisher);
                        return RedirectToAction("Index");
                    }
                }    
            }            

            return View("Edit", publisher);             
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            //always use Unit of work for save/update
            using (UnitOfWorkScope.Start())
            {
                var repo = ServiceLocator.Current.GetInstance<IRepository<Publisher>>();
                repo.Delete(id);
            }
            

            return RedirectToAction("Index");
        }
        
        public ActionResult Details(long id)
        {
            var publisher = _repository.Get(id);  

            return View(publisher);
        }

    }
}
