using System.Web.Mvc;

namespace Zephyr.Web.Mvc.Html
{
    public static class HtmlHelperExtensions
    {
         public static ZephyrHtmlHelper<TModel> Zephyr<TModel>(this HtmlHelper<TModel> helper) where TModel : class
         {
             return new ZephyrHtmlHelper<TModel>(helper);
         }
    }
}