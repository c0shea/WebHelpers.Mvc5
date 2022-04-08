using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace WebHelpers.Mvc5.JqGrid
{
    public class JqGridBuilder<TModel>
    {
        #region Render to View

        /// <summary>
        /// Renders the grid container.
        /// </summary>
        public IHtmlContent Render(Grid grid)
        {
            return Container(grid);
        }

        /// <summary>
        /// Initializes the grid via JavaScript after the page has loaded.
        /// jQuery must be defined before this method in your view or layout for this to work.
        /// </summary>
        public IHtmlContent Initialize(Grid grid)
        {
            return Script(grid);
        }

        /// <summary>
        /// Renders the grid container and initializes the grid all at once.
        /// jQuery must be defined before this method in your view or layout for this to work.
        /// </summary>
        public IHtmlContent RenderAndInitialize(Grid grid)
        {
            return new HtmlContentBuilder().AppendHtml(Container(grid)).AppendHtml(Script(grid));
        }

        private IHtmlContent Container(Grid grid)
        {
            var div = new TagBuilder("div");
            div.GenerateId($"{grid.Name}-container", "");

            var table = new TagBuilder("table");
            table.GenerateId(grid.Name, "");

            div.InnerHtml.AppendHtml(table);

            if (grid.ShowPager)
            {
                var pager = new TagBuilder("div");
                pager.GenerateId(grid.Pager, "");
                div.InnerHtml.AppendHtml(pager);
            }

            
            return div.RenderBody();
        }

        private IHtmlContent Script(Grid grid)
        {
            var script = new TagBuilder("script");

            script.MergeAttribute("type", "text/javascript");
            script.InnerHtml.Append(DocumentReady(grid));

            return script.RenderBody();
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

        #endregion

        #region Fluent Syntax

        public Grid Grid { get; set; }

        public void Columns(Action<List<Column>> columns)
        {
            if (Grid.Columns == null)
            {
                Grid.Columns = new List<Column>();
            }

            columns(Grid.Columns);
        }

        public Column ColumnFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            var propertyName = expression.GetExpressionText();

            return new Column(propertyName)
            {
                Label = Helper.PascalCaseToLabel(propertyName),
                SortType = Helper.MapSortType<TProperty>()
            };
        }

        #endregion
    }
}
