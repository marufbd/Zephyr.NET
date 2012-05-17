using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Zephyr.Data.Models;
using Zephyr.Web.Mvc.Html.Models;

namespace Zephyr.Web.Mvc.Html
{
    public static class PagerHtmlHelper
    {
        public static MvcHtmlString Pager(this ZephyrHtmlHelper zephyrHelper, IPagedList model)
        {
            return zephyrHelper.CreateHtmlHelperForModel(model).DisplayForModel("Pager", model);
        }
    }
}