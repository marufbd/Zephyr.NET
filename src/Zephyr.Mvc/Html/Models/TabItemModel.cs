using System.Web.Mvc;

namespace Zephyr.Web.Mvc.Html.Models
{
    public class TabItemModel
    {
        public string Title { get; set; }
        public bool Active { get; set; }
        public MvcHtmlString HtmlContent { get; set; } 

        public string TemplateName { get; set; }
        
        public string ActionName { get; set; }
        public string ControllerName { get; set; }        
    }
}