using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WebHelpers.Mvc5.JqGrid
{
    public class JqGridHelper
    {
        public IHtmlString Render(Grid grid)
        {
            return new MvcHtmlString(Container(grid) + Script(grid));
        }

        private string Container(Grid grid)
        {
            var div = new TagBuilder("div");
            div.GenerateId($"{grid.Name}-container");

            var table = new TagBuilder("table");
            table.GenerateId(grid.Name);

            div.InnerHtml += table.ToString();

            if (grid.ShowPager)
            {
                var pager = new TagBuilder("div");
                pager.GenerateId(grid.Pager);
                div.InnerHtml += pager.ToString();
            }

            return div.ToString();
        }

        private string Script(Grid grid)
        {
            var script = new TagBuilder("script");

            script.MergeAttribute("type", "text/javascript");
            script.InnerHtml = Initialize(grid);

            return script.ToString();
        }

        // TODO: This isn't working because $ isn't defined yet since this is inserted into the DOM before the jQuery script tag. Need to provide an option to defer.
        private string Initialize(Grid grid)
        {
            var sb = new StringBuilder();
            sb.AppendLine("$(document).ready(function () {");

            sb.Append(@"$(""#");
            sb.Append(grid.Name);
            sb.Append(@""").jqGrid(");
            sb.Append(grid);
            sb.AppendLine(");");

            sb.AppendLine("});");

            return sb.ToString();
        }
    }
}
