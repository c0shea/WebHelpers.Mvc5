using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using WebHelpers.Mvc5.JqGrid;

namespace WebHelpers.Mvc5
{
    /// <summary>
    /// Extensions for <see cref="HtmlHelper"/>.
    /// </summary>
    public static class HtmlHelperExtensions
    {
        public static IHtmlString JqGrid(this HtmlHelper helper)
        {
            var grid = new Grid
            {
                /*
                 * colModel: [
                    { label: 'Category Name', name: 'CategoryName', width: 100, frozen: true },
                    { label: 'Product Name', name: 'ProductName', width: 150, frozen: true },
                    { label: 'Country', name: 'Country', width: 200 },
                    { label: 'Price', name: 'Price', width: 250, sorttype: 'number' },
                    { label: 'Quantity', name: 'Quantity', width: 250, sorttype: 'integer' }                   
                ],
                 */
                Columns = new List<Column>
                {
                    new Column
                    {
                        Name = "CategoryName"
                    },
                    new Column
                    {
                        Name = "ProductName"
                    },
                    new Column
                    {
                        Name = "Country"
                    },
                    new Column
                    {
                        Name = "Price"
                    },
                    new Column
                    {
                        Name = "Quantity"
                    },
                    new Column
                    {
                        Name = "ACT",
                        FormatterName = "Fmt"
                    }
                    //new Column
                    //{
                    //    Name = "Second",
                    //    Align = TextAlign.Right,
                    //    IsHidden = true
                    //},
                    //new Column
                    //{
                    //    Name = "Test",
                    //    Class = "ui-ellipsis",
                    //    IsEditable = true,
                    //    EditAttributes = new { cacheDataUrl = true, delimiter = "|" },
                    //    FormatterName = IntegerColumnFormatOptions.Name,
                    //    FormatOptions = new IntegerColumnFormatOptions
                    //    {
                    //        ThousandsSeparator = " "
                    //    }
                    //},
                    //new Column
                    //{
                    //    Name = "LinkCol",
                    //    FormatterName = ActionColumnFormatOptions.Name,
                    //    FormatOptions = new ActionColumnFormatOptions
                    //    {
                    //        IsKeyBindingEnabled = true
                    //    }
                    //}
                }
            };

            var serializer = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            };
            using (var stringWriter = new StringWriter())
            using (var writer = new JsonTextWriter(stringWriter) { QuoteName = false })
            {
                serializer.Serialize(writer, grid);

                var json = stringWriter.ToString();

                return new MvcHtmlString(json);
            }
        }
    }
}
