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
        public ActionResult Edit(Guid guid)
        {
            Publisher publisher = _repository.Get(guid);

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
        
        public ActionResult Details(Guid guid)
        {
            var publisher = _repository.Get(guid);

            return View(publisher);
        }

        [HttpPost]
        public ActionResult Delete(Guid guid)
        {
            //always use Unit of work for save/update
            using (UnitOfWorkScope.Start())
            {
                var repo = ServiceLocator.Current.GetInstance<IRepository<Publisher>>();
                repo.Delete(guid);
            }


            return RedirectToAction("Index");
        }
    }
}
