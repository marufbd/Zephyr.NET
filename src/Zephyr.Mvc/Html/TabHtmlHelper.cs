using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Zephyr.Web.Mvc.Html.Models;

namespace Zephyr.Web.Mvc.Html
{
    public static class TabHtmlHelper
    {
        public static MvcHtmlString Tabs(this ZephyrHtmlHelper titanHelper, TabItemModel[] items)
        {
            var model = new TabHelperModel(items);
            foreach (var tabItemModel in model)
            {
                //tabItemModel.HtmlContent = titanHelper.HtmlHelper.Action("Index", tabItemModel.ControllerName);
                
                //need to implement controller resolved content
                if(tabItemModel.HtmlContent==null)
                    tabItemModel.HtmlContent = new MvcHtmlString("sample content");
            }

            return titanHelper.CreateHtmlHelperForModel(model).DisplayForModel(model);
        }
    }
}