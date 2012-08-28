using System;
using System.Web.Mvc;
using DemoApp.Web.DomainModels;
using Microsoft.Practices.ServiceLocation;
using Zephyr.Data.Repository;
using Zephyr.Data.Repository.Contract;
using Zephyr.Data.UnitOfWork;
using Zephyr.Web.Mvc.Controllers;
using System.Linq;
using Zephyr.Web.Mvc.Extentions;
using Zephyr.Web.Mvc.Html.Flash;
using Zephyr.Web.Mvc.ViewModels;

namespace DemoApp.Web.Controllers
{    
    public class PublisherController : ZephyrCRUDController<Publisher>
    {        
        
    }
}
