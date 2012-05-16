using System;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;
using Zephyr.Data.Repository.Contract;
using Zephyr.Data.UnitOfWork;
using Zephyr.Domain;
using Zephyr.Web.Mvc.ViewModels;

namespace Zephyr.Web.Mvc.Controllers
{
    public class ZephyrCRUDController<TEntity> : ZephyrController where TEntity : DomainEntity
    {
        public readonly IRepository<TEntity> Repository;

        public ZephyrCRUDController()
        {
            Repository = ServiceLocator.Current.GetInstance<IRepository<TEntity>>();
        }

        public ZephyrCRUDController(IRepository<TEntity> repository)
        {
            Repository = repository;
        }

        public virtual ActionResult List(int page=1, int items=10)
        {
            var viewModel = new ListViewModel<TEntity>() {Model = Repository.GetAll()};
            
            return View("List", viewModel);
        }

        public virtual ActionResult Details(Guid guid)
        {
            var viewModel = new DetailsViewModel<TEntity>() { Model = Repository.Get(guid) };

            return View("Details", viewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {            
            if(ViewEngines.Engines.FindView(ControllerContext, "Create", null).View!=null)
            {
                var viewModel = new CreateViewModel<TEntity>() { Model = Activator.CreateInstance<TEntity>() };

                return View("Create", viewModel);
            }
            else
            {
                var viewModel = new EditViewModel<TEntity>() { Model = Activator.CreateInstance<TEntity>() };

                return View("Edit", viewModel);
            }
        }

        [HttpGet]
        public ActionResult Edit(Guid guid)
        {            
            var viewModel = new EditViewModel<TEntity>() { Model = Repository.Get(guid) }; 

            return View("Edit", viewModel);
        }

        [HttpPost]
        public ActionResult Delete(Guid guid)
        {
            //always use Unit of work for save/update
            using (UnitOfWorkScope.Start())
            {
                var repo = ServiceLocator.Current.GetInstance<IRepository<TEntity>>();
                repo.Delete(guid);
            }

            return RedirectToAction("List");
        }
    }
}