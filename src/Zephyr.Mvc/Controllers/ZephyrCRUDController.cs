using System.Web.Mvc;

namespace Zephyr.Web.Mvc.Controllers
{
    public class ZephyrCRUDController : ZephyrController
    {
         public virtual ActionResult Edit(string guid)
         {
             return new EmptyResult();
         }
    }
}