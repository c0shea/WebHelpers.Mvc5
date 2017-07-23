using System.Web.Mvc;

namespace WebHelpers.Mvc5
{
    /// <summary>
    /// Extensions for <see cref="UrlHelper"/>.
    /// </summary>
    public static class UrlHelperExtensions
    {
        /// <summary>
        /// Gets the CSS class to use for the link state of the specified <paramref name="action"/>.
        /// If the current request route matches the <paramref name="action"/> and <paramref name="controller"/>,
        /// the "active" class is returned.
        /// </summary>
        /// <param name="url">The <see cref="UrlHelper"/>.</param>
        /// <param name="action">The action of the link to compare to the current request.</param>
        /// <param name="controller">The controller of the link to compare to the current request.</param>
        public static string IsLinkActive(this UrlHelper url, string action, string controller)
        {
            if (url.RequestContext.RouteData.Values["controller"].ToString() == controller &&
                url.RequestContext.RouteData.Values["action"].ToString() == action)
            {
                return "active";
            }

            return "";
        }
    }
}
