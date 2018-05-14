using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebHelpers.Mvc5.JqGrid
{
    /// <remarks>
    /// http://www.guriddo.net/documentation/guriddo/javascript/user-guide/basic-grid/#options
    /// </remarks>
    public class Grid
    {
        // TODO: addOptions
        // TODO: ajaxCellOptions
        // TODO: ajaxGridOptions
        // TODO: ajaxRowOptions
        // TODO: ajaxSelectOptions
        // TODO: ajaxSubgridOptions

        /// <summary>
        /// Specifies whether or not to zebra stripe alternate rows.
        /// </summary>
        [JsonProperty("altRows")]
        public bool ShouldAlternateRowStyle { get; set; }

        /// <summary>
        /// Specifies whether or not request and response data should be HTML encoded.
        /// </summary>
        [JsonProperty("autoencode")]
        public bool ShouldAutoEncode { get; set; }

        /// <summary>
        /// Specifies whether or not the grid's width is calculated automatically to the width
        /// of the parent element on initial grid creation. In order to resize the grid when the
        /// parent element or window changes, use the <see cref="Responsive"/> parameter instead.
        /// </summary>
        [JsonProperty("autowidth")]
        public bool IsInitialWidthAutomatic { get; set; }

        /// <summary>
        /// The caption for the grid that appears above the column headers.
        /// </summary>
        /// <remarks>
        /// http://www.guriddo.net/documentation/guriddo/javascript/#how-it-works
        /// </remarks>
        [JsonProperty("caption")]
        public string Caption { get; set; }

        /// <summary>
        /// The padding + border width of the cell. Usually this should not be changed, but if custom
        /// changes to the td element are made in the grid CSS, this will need to be adjusted.
        /// </summary>
        [JsonProperty("cellLayout")]
        [DefaultValue(5)]
        public int CellLayoutSize { get; set; } = 5;

        /// <summary>
        /// Specifies whether or not cell editing is enabled.
        /// </summary>
        [JsonProperty("cellEdit")]
        public bool CanEdit { get; set; }

        /// <summary>
        /// Specifies where the contents of the cell should be saved.
        /// In the case of <see cref="DataDestination.Remote"/>, the data is saved via an AJAX call.
        /// </summary>
        [JsonProperty("cellsubmit")]
        [DefaultValue(DataDestination.Remote)]
        public DataDestination CellSubmitDestination { get; set; } = DataDestination.Remote;

        /// <summary>
        /// The URL where the cell is saved when the <see cref="CellSubmitDestination"/> is set
        /// to <see cref="DataDestination.Remote"/>.
        /// </summary>
        [JsonProperty("cellurl")]
        public string CellSubmitUrl { get; set; }

        // TODO: cmTemplate
        // TODO: colFilters
        // TODO: colMenu



        [JsonProperty("colNames")]
        public List<string> ColumnNames => Columns?.Select(c => c.Label ?? c.Name).ToList();

        [JsonProperty("colModel")]
        public List<Column> Columns { get; set; }

        private bool IsValid()
        {
            if (Columns != null)
            {
                var formEditOptions = Columns.Where(c => c.FormEditOptions != null).Select(c => c.FormEditOptions);

                // TODO: Ensure that there are no duplicate row:column position combinations across the columns

                return Columns.Count(c => c.IsPrimaryKey) <= 1 &&
                       Columns.Count == ColumnNames.Count;
            }

            if (CellSubmitUrl != null && CellSubmitDestination != DataDestination.Remote)
            {
                return false;
            }

            return true;
        }

        public override string ToString()
        {
            var serializer = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            };
            using (var stringWriter = new StringWriter())
            using (var writer = new JsonTextWriter(stringWriter) { QuoteName = false })
            {
                serializer.Serialize(writer, this);

                return stringWriter.ToString();
            }
        }
    }
}
