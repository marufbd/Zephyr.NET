using System.Web.Mvc;

namespace Zephyr.Web.Mvc.Html
{
    public static class HtmlHelperExtensions
    {
         public static ZephyrHtmlHelper Zephyr(this HtmlHelper helper)
         {             
             return new ZephyrHtmlHelper(helper);
         }
    }
}