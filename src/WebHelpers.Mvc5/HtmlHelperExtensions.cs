using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
        public static WebHelpers<TModel> WebHelpers<TModel>(this IHtmlHelper<TModel> htmlHelper)
        {
            return new WebHelpers<TModel>();
        }

        /// <summary>
        /// Constructs a new WebHelpers instance using the type specified by <typeparamref name="T"/>.
        /// </summary>
        public static WebHelpers<T> WebHelpers<T>(this IHtmlHelper htmlHelper)
        {
            return new WebHelpers<T>();
        }
    }
}
