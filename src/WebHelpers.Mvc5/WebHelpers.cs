using System.Web.Mvc;
using WebHelpers.Mvc5.JqGrid;

namespace WebHelpers.Mvc5
{
    /// <summary>
    /// Custom HtmlHelpers encapsulated in the WebHelpers namespace. This provides a layer
    /// to group the helpers to prevent polluting the base <see cref="HtmlHelper"/> class.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public class WebHelpers<TModel>
    {
        public static JqGridBuilder<TModel> JqGrid()
        {
            return new JqGridBuilder<TModel>();
        }
    }
}
