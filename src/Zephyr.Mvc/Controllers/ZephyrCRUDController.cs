using System;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;
using Zephyr.Data.Models;
using Zephyr.Data.Repository.Contract;
using Zephyr.Data.UnitOfWork;
using Zephyr.Domain;
using Zephyr.Web.Mvc.Extentions;
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

        public virtual ActionResult List(SortOptions sortOptions, int page, int items)
        {            
            var viewModel = new ListViewModel<TEntity>()
                                {
                                    Model =
                                        String.IsNullOrEmpty(sortOptions.SortProperty)
                                            ? Repository.GetAllPaged(page, items)
                                            : Repository.GetAllPaged(page, items, sortOptions)
                                };
            
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
        public ActionResult Edit(TEntity entity)
        {
            if (ModelState.IsValid)
            {
                bool edit = entity.IsNew;
                string flashMsg = entity.IsNew
                                      ? "New <strong>" + entity.GetType().Name + "</strong> added successfully"
                                      : "<strong>" + entity.GetType().Name + "</strong> saved successfully";
                //always use Unit of work for save/update
                using (UnitOfWorkScope.Start())
                {
                    var repo = ServiceLocator.Current.GetInstance<IRepository<TEntity>>();

                    repo.SaveOrUpdate(entity);

                    return RedirectToAction("List").WithFlash(new { alert_success = flashMsg });
                }
            }

            return View("Edit", new EditViewModel<TEntity>() { Model = entity });
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

            return RedirectToAction("List").WithFlash(new {alert_success = "Item deleted !"});
        }
    }
}