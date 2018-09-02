using System.Web.Mvc;
using WebHelpers.Mvc5.JqGrid;

namespace WebHelpers.Mvc5
{
    /// <summary>
    /// Extensions for <see cref="HtmlHelper"/>.
    /// </summary>
    public static class HtmlHelperExtensions
    {
        public static JqGridHelper JqGrid(this HtmlHelper helper)
        {
            return new JqGridHelper();
        }
    }
}
