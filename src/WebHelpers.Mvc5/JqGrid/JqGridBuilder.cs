using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace WebHelpers.Mvc5.JqGrid
{
    public class JqGridBuilder<TModel>
    {
        #region Render to View

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
            var propertyName = ExpressionHelper.GetExpressionText(expression);

            return new Column(propertyName)
            {
                Label = Helper.PascalCaseToLabel(propertyName),
                SortType = Helper.MapSortType<TProperty>()
            };
        }

        #endregion
    }
}
