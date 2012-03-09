using System.Web.Mvc;

namespace Zephyr.Web.Mvc.Html
{
    public class ZephyrHtmlHelper
    {
        public HtmlHelper HtmlHelper { get; protected set; }

        public ZephyrHtmlHelper(HtmlHelper helper)
        {
            //titan helper initilization
            ZephyrViewEngine.Register();
            this.HtmlHelper = helper;
        }

        /// <summary>
        /// Creates the HTML helper for model.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public HtmlHelper<TModel> CreateHtmlHelperForModel<TModel>(TModel model)
        {
            return new HtmlHelper<TModel>(HtmlHelper.ViewContext, new ViewDataContainer<TModel>(model));
        }
    }
}