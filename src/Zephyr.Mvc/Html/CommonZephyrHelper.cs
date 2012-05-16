using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Zephyr.Web.Mvc.Html.Models;

namespace Zephyr.Web.Mvc.Html
{
    public static class CommonZephyrlHelper
    {
        public static bool ExistsView(this ZephyrHtmlHelper zephyrHelper, string viewName)
        {
            ViewEngineResult viewResult =
                ViewEngines.Engines.FindView(zephyrHelper.HtmlHelper.ViewContext.Controller.ControllerContext, viewName,
                                             null);

            return viewResult.View != null;
        }


        public static MvcHtmlString Flash(this ZephyrHtmlHelper zephyrHelper, string tagName="div")
        {
            var msg = zephyrHelper.HtmlHelper.ViewContext.TempData["Message"];

            return msg==null
                       ? MvcHtmlString.Empty
                       : new MvcHtmlString("<div class=\"alert alert-success\">" + msg + "</div>");
        }

        /// <summary>
        /// Html helper for generating drop down list for Enum type model property.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="defaultText">Default text for empty item on nullable enum type property </param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, string defaultText="", object htmlAttributes=null)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            
            Type enumType = Nullable.GetUnderlyingType(metadata.ModelType) ?? metadata.ModelType;
            
            IEnumerable<TEnum> values = Enum.GetValues(enumType).Cast<TEnum>();

            IList<SelectListItem> items = values.Select(val=>new SelectListItem
                                                            {
                                                                Text = val.ToString(),
                                                                Value = val.ToString(),
                                                                Selected = val.Equals(metadata.Model)
                                                            }).ToList();


            if (metadata.Model is Nullable && metadata.Model == null)
            {
                items.Insert(0, new SelectListItem { Value = "", Text = defaultText, Selected = true });
            }

            // If the enum is nullable, add an 'empty' item to the collection
            if (metadata.IsNullableValueType)
            {
                items.Insert(0, new SelectListItem { Value = "", Text = defaultText });
                //items = _singleEmptyItem.Concat(items).ToList();
            }


            return htmlHelper.DropDownListFor(expression, items, htmlAttributes);
        }
    }
}