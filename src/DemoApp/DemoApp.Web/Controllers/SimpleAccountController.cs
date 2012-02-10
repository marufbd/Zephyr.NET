using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using DemoApp.Web.DomainModels;
using DemoApp.Web.ViewModels;
using Zephyr.Data.Repository;
using Zephyr.Data.Repository.Contract;

namespace DemoApp.Web.Controllers
{
    public class SimpleAccountController : Controller
    {
        private readonly IRepository<User> _repository=new NhRepository<User>();
        //
        // GET: /SimpleAccount/

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (
                    _repository.GetAll().AsQueryable().Any(
                        m => m.Username.Equals(model.UserName) && m.Password.Equals(model.Password)))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}
