using System;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;
using Zephyr.Data.Repository.Contract;
using Zephyr.Data.UnitOfWork;
using Zephyr.Domain;

namespace Zephyr.Web.Mvc.Controllers
{
    public class ZephyrCRUDController<TEntity> : ZephyrController where TEntity : Entity
    {
        public readonly IRepository<TEntity> Repository;

        public ZephyrCRUDController(IRepository<TEntity> repository)
        {
            Repository = repository;
        }

        public virtual ActionResult List()
        {            
            return View("List", Repository.GetAll());
        }

        public virtual ActionResult Details(Guid guid)
        {
            return View("Details", Repository.Get(guid));
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = Activator.CreateInstance<TEntity>();

            return View("Edit", model);
        }

        [HttpGet]
        public ActionResult Edit(Guid guid)
        {
            TEntity model = Repository.Get(guid);

            return View("Edit", model);
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