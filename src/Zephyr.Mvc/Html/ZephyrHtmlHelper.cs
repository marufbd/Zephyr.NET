using System.Web.Mvc;

namespace Zephyr.Web.Mvc.Html
{
    public class ZephyrHtmlHelper<TModel> where TModel : class 
    {
        public HtmlHelper<TModel> HtmlHelper { get; protected set; }

        public ZephyrHtmlHelper(HtmlHelper<TModel> helper)
        {
            //Zephyr helper initilization
            ZephyrViewEngine.Register();
            this.HtmlHelper = helper;
        }

     
        /// <summary>
        /// Creates the HTML helper for model.
        /// </summary>
        /// <typeparam name="T">The type of the model.</typeparam>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public HtmlHelper<T> CreateHtmlHelperForModel<T>(T model)
        {
            return new HtmlHelper<T>(HtmlHelper.ViewContext, new ViewDataContainer<T>(model));
        }
    }
}