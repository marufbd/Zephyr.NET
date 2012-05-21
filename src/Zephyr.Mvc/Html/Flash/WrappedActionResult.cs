using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Zephyr.DesignByContract;

namespace Zephyr.Web.Mvc.Html.Flash
{
    public class WrappedActionResultWithFlash<TActionResult> : ActionResult where TActionResult : ActionResult
    {
        public WrappedActionResultWithFlash(TActionResult wrappingResult, IDictionary<string, string> flashMessages)
        {
            Check.Require(wrappingResult!=null, "Argument Actionresult cannot be null", new ArgumentNullException("wrappingResult"));
            Check.Require(wrappingResult != null, "Argument FlashMessage cannot be null", new ArgumentNullException("flashMessages"));
            

            WrappingResult = wrappingResult;
            FlashMessages = flashMessages;
        }

        public TActionResult WrappingResult { get; private set; }

        public IDictionary<string, string> FlashMessages { get; private set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var storage = new FlashStorage(context.Controller.TempData);

            foreach (var pair in FlashMessages)
            {
                storage.Add(pair.Key, pair.Value);
            }

            WrappingResult.ExecuteResult(context);
        }
    }
}