using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace WebHelpers.Mvc5.JqGrid
{
    public class JqGridHelper<TModel>
    {
        // Credit: Humanizer (https://github.com/Humanizr/Humanizer/blob/master/src/Humanizer/StringHumanizeExtensions.cs#L16)
        // ReSharper disable once StaticMemberInGenericType
        private static readonly Regex PascalCaseWordPartsRegex = new Regex(@"[\p{Lu}]?[\p{Ll}]+|[0-9]+[\p{Ll}]*|[\p{Lu}]+(?=[\p{Lu}][\p{Ll}]|[0-9]|\b)|[\p{Lo}]+",
            RegexOptions.IgnorePatternWhitespace | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Renders the grid container.
        /// </summary>
        public IHtmlString Render(Grid grid)
        {
            return new MvcHtmlString(Container(grid));
        }

        /// <summary>
        /// Initializes the grid via JavaScript after the page has loaded.
        /// jQuery must be defined before this method in your view or layout for this to work.
        /// </summary>
        public IHtmlString Initialize(Grid grid)
        {
            return new MvcHtmlString(Script(grid));
        }

        /// <summary>
        /// Renders the grid container and initializes the grid all at once.
        /// jQuery must be defined before this method in your view or layout for this to work.
        /// </summary>
        public IHtmlString RenderAndInitialize(Grid grid)
        {
            return new MvcHtmlString(Container(grid) + Script(grid));
        }

        public Column ColumnFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            var propertyName = ExpressionHelper.GetExpressionText(expression);

            return new Column(propertyName)
            {
                Label = FromPascalCase(propertyName),
                
            };
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
            script.InnerHtml = DocumentReady(grid);

            return script.ToString();
        }

        private string DocumentReady(Grid grid)
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

        private static string FromPascalCase(string input)
        {
            var result = string.Join(" ", PascalCaseWordPartsRegex
                .Matches(input).Cast<Match>()
                .Select(match => match.Value.ToCharArray().All(char.IsUpper) &&
                                 (match.Value.Length > 1 || (match.Index > 0 && input[match.Index - 1] == ' ') || match.Value == "I")
                    ? match.Value
                    : match.Value.ToLower()));

            return result.Length > 0 ? char.ToUpper(result[0]) +
                                       result.Substring(1, result.Length - 1) : result;
        }
    }
}
