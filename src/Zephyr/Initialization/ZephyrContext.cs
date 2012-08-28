using System.Security.Principal;
using System.Threading;
using System.Web;

namespace Zephyr.Initialization
{
    public static class ZephyrContext
    {
        /// <summary>
        /// Gets or Sets the current <see also cref="IZephyrPrincipal"/>.
        /// </summary>
        public static IPrincipal User
        {
            get
            {
                return IsWebApplication
                    ? HttpContext.Current.User ?? Thread.CurrentPrincipal
                            : Thread.CurrentPrincipal;
            }
            set
            {
                if (IsWebApplication)
                {
                    //Thread.CurrentPrincipal = value;
                    HttpContext.Current.User = value;
                }
                else
                {
                    Thread.CurrentPrincipal = value;
                }
            }
        }

        /// <summary>
        /// Gets the IP address.
        /// </summary>
        /// <returns></returns>
        public static string GetIPAddress()
        {
            HttpContext context = HttpContext.Current;

            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        /// <summary>
        /// Gets a value indicating whether this instance is web application.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is web application; otherwise, <c>false</c>.
        /// </value>
        public static bool IsWebApplication
        {
            get { return HttpContext.Current != null; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is initializing.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is initializing; otherwise, <c>false</c>.
        /// </value>
        public static bool IsInitializing { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is test mode.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is test mode; otherwise, <c>false</c>.
        /// </value>
        public static bool IsTestMode { get; set; }
    }
}
