using System.Collections.Generic;

namespace Zephyr.Web.Mvc.Html.Models
{
    public class TabHelperModel : List<TabItemModel>
    {
         public TabHelperModel(IEnumerable<TabItemModel> items):base(items)
         {
             
         }
    }
}