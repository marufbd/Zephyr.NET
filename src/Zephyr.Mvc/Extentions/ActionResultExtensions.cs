using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Zephyr.Web.Mvc.Html.Flash;

namespace Zephyr.Web.Mvc.Extentions
{
    public static class ActionResultExtensions
    {
        public static WrappedActionResultWithFlash<RedirectResult> WithFlash(this RedirectResult instance, object arguments)
        {
            return Flash(instance, ToDictionary(arguments));
        }

        public static WrappedActionResultWithFlash<RedirectResult> WithFlash(this RedirectResult instance, IDictionary<string, string> arguments)
        {
            return Flash(instance, arguments);
        }

        public static WrappedActionResultWithFlash<RedirectToRouteResult> WithFlash(this RedirectToRouteResult instance, object arguments)
        {
            return Flash(instance, ToDictionary(arguments));
        }

        public static WrappedActionResultWithFlash<RedirectToRouteResult> WithFlash(this RedirectToRouteResult instance, IDictionary<string, string> arguments)
        {
            return Flash(instance, arguments);
        }

        public static WrappedActionResultWithFlash<ViewResult> WithFlash(this ViewResult instance, object arguments)
        {
            return Flash(instance, ToDictionary(arguments));
        }

        public static WrappedActionResultWithFlash<ViewResult> WithFlash(this ViewResult instance, IDictionary<string, string> arguments)
        {
            return Flash(instance, arguments);
        }

        private static WrappedActionResultWithFlash<TActionResult> Flash<TActionResult>(TActionResult instance, IDictionary<string, string> arguments) where TActionResult : ActionResult
        {
            return new WrappedActionResultWithFlash<TActionResult>(instance, arguments);
        }

        private static IDictionary<string, string> ToDictionary(object arguments)
        {
            if (arguments == null)
            {
                return new Dictionary<string, string>();
            }

            return arguments.GetType()
                            .GetProperties()
                            .Where(p => p.CanRead && p.GetIndexParameters().Length == 0)
                            .ToDictionary(p => p.Name, p => p.GetValue(arguments, null).ToString());
        }
    }
}