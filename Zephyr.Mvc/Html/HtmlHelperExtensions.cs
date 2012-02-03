using System.Web.Mvc;

namespace Zephyr.Web.Mvc.Html
{
    public static class HtmlHelperExtensions
    {
         public static TitanHtmlHelper Titan(this HtmlHelper helper)
         {             
             return new TitanHtmlHelper(helper);
         }
    }
}