using System.Web.Mvc;

namespace WebHelpers.Mvc5
{
    /// <summary>
    /// Extensions for <see cref="HtmlHelper"/>.
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Constructs a new WebHelpers instance using the type of this view's model.
        /// </summary>
        public static WebHelpers<TModel> WebHelpers<TModel>(this HtmlHelper<TModel> htmlHelper)
        {
            return new WebHelpers<TModel>();
        }

        /// <summary>
        /// Constructs a new WebHelpers instance using the type specified by <typeparamref name="T"/>.
        /// </summary>
        public static WebHelpers<T> WebHelpers<T>(this HtmlHelper htmlHelper)
        {
            return new WebHelpers<T>();
        }
    }
}
