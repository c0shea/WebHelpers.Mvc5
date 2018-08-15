using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using JetBrains.Annotations;

namespace WebHelpers.Mvc5
{
    /// <summary>
    /// Extensions for <see cref="UrlHelper"/>.
    /// </summary>
    public static class UrlHelperExtensions
    {
        /// <summary>
        /// Gets the CSS class to use for the link state of the specified <paramref name="actionName"/>.
        /// If the current request route matches the <paramref name="actionName"/> and <paramref name="controllerName"/>,
        /// the "active" class is returned.
        /// </summary>
        /// <param name="url">The <see cref="UrlHelper"/>.</param>
        /// <param name="actionName">The action of the link to compare to the current request.</param>
        /// <param name="controllerName">The controller of the link to compare to the current request.</param>
        public static string IsLinkActive(this UrlHelper url, [AspMvcAction] string actionName, [AspMvcController] string controllerName)
        {
            if (url.RequestContext.RouteData.Values["controller"].ToString() == controllerName &&
                url.RequestContext.RouteData.Values["action"].ToString() == actionName)
            {
                return "active";
            }

            return "";
        }

        /// <summary>
        /// Gets the CSS class to use for the treeview state. If the current request route matches
        /// any of the <paramref name="actions"/>, the "active" class is returned.
        /// </summary>
        /// <param name="url">The <see cref="UrlHelper"/>.</param>
        /// <param name="actions">A collection of KeyValuePairs, where the key is the action and the value is the controller.</param>
        /// <returns></returns>
        public static string IsTreeviewActive(this UrlHelper url, Dictionary<string, string> actions)
        {
            var controller = url.RequestContext.RouteData.Values["controller"].ToString();
            var action = url.RequestContext.RouteData.Values["action"].ToString();

            if (actions.Any(a => a.Key == action && a.Value == controller))
            {
                return "active";
            }

            return "";
        }
    }
}
