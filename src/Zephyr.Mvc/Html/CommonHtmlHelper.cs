using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Zephyr.Web.Mvc.Html.Models;

namespace Zephyr.Web.Mvc.Html
{
    public static class CommonHtmlHelper
    {
        public static MvcHtmlString PageTitle<TModel>(this ZephyrHtmlHelper<TModel> titanHelper) where TModel : class
        {
            var appName = Zephyr.Configuration.ZephyrConfiguration.ZephyrSettings.AppName;
            return new MvcHtmlString(appName + " - " + titanHelper.HtmlHelper.ViewData["Title"]);
        }        
    }
}