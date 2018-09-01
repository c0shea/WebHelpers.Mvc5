using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using WebHelpers.Mvc5.JqGrid.Converters;
using TimeSpanConverter = WebHelpers.Mvc5.JqGrid.Converters.TimeSpanConverter;

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

        /// <summary>
        /// Defines a template of properties that override the default values for all columns.
        /// </summary>
        [JsonProperty("cmTemplate")]
        public object ColumnTemplate { get; set; }

        // TODO: colFilters

        /// <summary>
        /// Specifies whether or not the column menu is enabled. The column menu creates a button
        /// on the header of allowed columns and provides a context menu when clicked.
        /// </summary>
        /// <remarks>
        /// http://www.guriddo.net/documentation/guriddo/javascript/user-guide/colmenu/
        /// </remarks>
        [JsonProperty("colMenu")]
        public bool ShowColumnMenu { get; set; }

        [JsonProperty("colModel")]
        public List<Column> Columns { get; set; }
        
        [JsonProperty("colNames")]
        public List<string> ColumnNames => Columns?.Select(c => c.Label ?? c.Name).ToList();

        [JsonProperty("data")]
        [JsonConverter(typeof(LiteralNameConverter))]
        public string LocalDataArrayName { get; set; }
        
        /// <summary>
        /// The string of data to use when the <see cref="DataType"/> parameter is set to
        /// <see cref="DataType.XmlLocalData"/> or <see cref="DataType.JsonLocalData"/>.
        /// </summary>
        [JsonProperty("datastr")]
        public string LocalData { get; set; }

        /// <summary>
        /// The data format expected to fill the grid.
        /// </summary>
        [JsonProperty("datatype")]
        [DefaultValue(DataType.Xml)]
        public DataType DataType { get; set; } = DataType.Xml;

        /// <summary>
        /// Specifies whether or not jQuery empty is used for the row and all child elements.
        /// This option should be set to true if an event or plugin is attached to the table cell.
        /// TODO: Set to true if sortable rows and/or columns are activated
        /// </summary>
        [JsonProperty("deepempty")]
        public bool UseJqueryEmpty { get; set; }

        // TODO: delOptions

        /// <summary>
        /// Specifies whether or not the currently selected row(s) are deselected when a sort is applied.
        /// This option is only applicable when the <see cref="DataType"/> is set to <see cref="DataType.LocalDataArray"/>.
        /// </summary>
        [JsonProperty("deselectAfterSort")]
        [DefaultValue(true)]
        public bool ShouldDeselectAfterSort { get; set; } = true;

        /// <summary>
        /// The direction of the text in the grid. The grid will automatically change the direction of the text
        /// depending on this option.
        /// </summary>
        [JsonProperty("direction")]
        [DefaultValue(JqGrid.TextDirection.LeftToRight)]
        public TextDirection TextDirection { get; set; } = TextDirection.LeftToRight;

        // TODO: editOptions

        [JsonProperty("editurl")]
        public string EditUrl { get; set; }

        /// <summary>
        /// The message to display when the returned or current number of records in the grid is zero.
        /// This option is only valid when TODO: viewrecords is true.
        /// </summary>
        [JsonProperty("emptyrecords")]
        public string EmptyResultSetMessage { get; set; }

        /// <summary>
        /// Specifies whether or not the tree grid is expanded or collapsed when the user clicks anywhere
        /// on the text in the expanded column. It is not necessary, then, to click exactly on the icon.
        /// </summary>
        [JsonProperty("ExpandColClick")]
        [DefaultValue(true)]
        public bool ShouldToggleOnColumnClick { get; set; } = true;

        /// <summary>
        /// Indicates which column should be used to expand the tree grid. The first column is the default.
        /// TODO: only when treegrid is true
        /// </summary>
        [JsonProperty("ExpandColumn")]
        [JsonConverter(typeof(ColumnToExpandConverter))]
        public Column ColumnToExpand { get; set; }

        public bool ShouldSerializeColumnToExpand() => ColumnToExpand?.Name != null;

        /// <summary>
        /// Specifies whether or not to show a footer row below the grid records and above the pager.
        /// The number of columns equal those specified in <see cref="Columns"/>.
        /// </summary>
        [JsonProperty("footerrow")]
        public bool ShowFooter { get; set; }

        /// <summary>
        /// Specifies whether or not the adjacent column to the right will resize when a column's width
        /// is changed so that the overall grid width is maintained. For example, reducing the width
        /// of column 2 by 30px will increase the size of column 3 by 30px. There is no horizontal
        /// scroll bar in this case. This option is not compatible with TODO: ShrinkToFit (ignore this if that one is true)
        /// </summary>
        [JsonProperty("forceFit")]
        public bool ShouldForceFit { get; set; }

        /// <summary>
        /// Specifies whether or not all the data is built and appended to the grid in the DOM in a single
        /// bulk operation as opposed to row-by-row. It is enabled by default for best performance, but
        /// it will not fire the afterInsertRow event. To use that event, set this option to false.
        /// </summary>
        [JsonProperty("gridview")]
        [DefaultValue(true)]
        public bool UseOptimizedRendering { get; set; } = true;

        /// <summary>
        /// Specifies whether or not grouping is enabled.
        /// </summary>
        [JsonProperty("grouping")]
        public bool CanGroup { get; set; }

        // TODO: groupingView

        /// <summary>
        /// Specifies whether or not the title attribute with the text from the column's label
        /// is added to the column headers.
        /// </summary>
        [JsonProperty("headertitles")]
        public bool ShowLabelOnColumnHeaderHover { get; set; }

        /// <summary>
        /// The height of the grid. Can be defined in pixels or as 100%. If 100% is specified,
        /// the vertical scrollbar doesn't appear. To change the height dynamically, use the
        /// setGridHeight method.
        /// </summary>
        [JsonProperty("height")]
        public Unit Height { get; set; }

        /// <summary>
        /// Specifies whether or not the grid is initially hidden. Data is not loaded and only the
        /// caption layer is shown. This option is only valid when <see cref="Caption"/> and
        /// <see cref="ShowGridToggleButton"/> are set.
        /// </summary>
        [JsonProperty("hiddengrid")]
        public bool IsHidden { get; set; }

        /// <summary>
        /// Specifies whether or not the grid show/hide toggle button appears to the right side of the
        /// caption layer. This option only has an effect when the <see cref="Caption"/> option is set.
        /// </summary>
        [JsonProperty("hidegrid")]
        [DefaultValue(true)]
        public bool ShowGridToggleButton { get; set; } = true;

        /// <summary>
        /// Specifies whether or not the hover effect is enabled when the mouse is hovered over a row.
        /// </summary>
        [JsonProperty("hoverrows")]
        [DefaultValue(true)]
        public bool UseHoverRowEffect { get; set; } = true;

        /// <summary>
        /// The icon set to use. This option is only valid when the // TODO: StyleUI is Bootstrap4.
        /// The appropriate icon set CSS file must be loaded for this to work.
        /// </summary>
        [JsonProperty("iconSet")]
        public IconSet? IconSet { get; set; }

        /// <summary>
        /// The string to add as a prefix to the ID of every grid row upon construction.
        /// This option is useful if two or more grids are rendered on the same page and there is
        /// the possibility of them having ID collisions and equal grid names.
        /// </summary>
        [JsonProperty("idPrefix")]
        public string IdPrefix { get; set; }

        /// <summary>
        /// Local searching is case-sensitive by default. To make local searching and sorting
        /// case-insensitive, set this option to true.
        /// </summary>
        [JsonProperty("ignoreCase")]
        public bool IsCaseInsensitive { get; set; }

        // TODO: inlineData
        // TODO: jsonReader (this should match the return type of the JSON model)
        // TODO: keyName

        /// <summary>
        /// Specifies whether or not the grid loads the data from the server only once and automatically
        /// changes to <see cref="DataDestination.Local"/> afterwards. All further manipulations
        /// are then done on the client side and the <see cref="LocalDataArrayName"/> is filled with
        /// the response data from the server.
        /// </summary>
        [JsonProperty("loadonce")]
        public bool ShouldLoadOnce { get; set; }

        /// <summary>
        /// The text to display in the progress indicator when requesting and sorting data.
        /// </summary>
        [JsonProperty("loadtext")]
        public string LoadingText { get; set; }

        /// <summary>
        /// Controls the load indicator when an AJAX operation is in progress.
        /// </summary>
        [JsonProperty("loadui")]
        [DefaultValue(LoadIndicator.Enable)]
        public LoadIndicator LoadIndicator { get; set; } = LoadIndicator.Enable;

        /// <summary>
        /// Specifies whether or not an icon is shown that allows user-defined actions.
        /// To add or remove actions from the menubar, use the menubarAdd and menubarDelete methods.
        /// </summary>
        [JsonProperty("menubar")]
        public bool ShowMenuBar { get; set; }

        /// <summary>
        /// The HTTP method to use when requesting data from the server.
        /// </summary>
        [JsonProperty("mtype")]
        [DefaultValue(HttpMethod.Get)]
        public HttpMethod HttpMethod { get; set; } = HttpMethod.Get;

        /// <summary>
        /// The minimum width of all grid columns when resizing.
        /// </summary>
        [JsonProperty("minColWidth")]
        [DefaultValue(33)]
        public int MinColumnWidth { get; set; } = 33;

        /// <summary>
        /// Specifies whether or not multi-selection is only done when the checkbox is clicked.
        /// Clicking on any other row deselects all rows and selects the current row.
        /// This option is only valid when the <see cref="CanMultiSelect"/> option is true.
        /// </summary>
        [JsonProperty("multiboxonly")]
        public bool SelectOnCheckBoxClickOnly { get; set; }

        /// <summary>
        /// The key that should be pressed to select multiple rows. This option is only valid
        /// when the <see cref="CanMultiSelect"/> option is true.
        /// </summary>
        [JsonProperty("multikey")]
        public MultiSelectKey? MultiSelectKey { get; set; }

        /// <summary>
        /// Specifies whether or not the selection of multiple rows is enabled.
        /// A new column with checkboxes is added to the left-most side of the grid.
        /// </summary>
        [JsonProperty("multiselect")]
        public bool CanMultiSelect { get; set; }

        /// <summary>
        /// Specifies whether or not sorting more than one field is enabled. If the data is
        /// obtained from a remote server, the // TODO: SIDX parameter contains the order clause.
        /// It is a comma-separated list (e.g. field1 asc, field2 desc, field3). Note that the
        /// sort order of the last column is specified in the // TODO: SORD parameter.
        /// </summary>
        [JsonProperty("multiSort")]
        public bool CanMultiSort { get; set; }

        /// <summary>
        /// The initial page number used when the request is made to retrieve data.
        /// </summary>
        [JsonProperty("page")]
        [DefaultValue(1)]
        public int InitialPageNumber { get; set; } = 1;

        /// <summary>
        /// The HTML ID reference to the pager bar used to navigate through the records.
        /// This must be a valid HTML element specified by its ID (e.g. #pager).
        /// </summary>
        // TODO: This should be automatically generated by the renderer and not manually specified here
        [JsonProperty("pager")]
        [DefaultValue("")]
        public string Pager { get; set; } = "";

        /// <summary>
        /// The position of the pager navigation buttons and record selection box in the grid.
        /// The pager element is divided into 3 positions and only one element can be in a single
        /// position. When changing this option, the <see cref="PagerRecordAlign"/> // TODO:  must be changed as well.
        /// </summary>
        [JsonProperty("pagerpos")]
        [DefaultValue(PagerAlign.Center)]
        public PagerAlign PagerNavigationAlign { get; set; } = PagerAlign.Center;

        /// <summary>
        /// Specifies whether or not the pager buttons should be shown if the pager is shown.
        /// </summary>
        [JsonProperty("pgbuttons")]
        [DefaultValue(true)]
        public bool ShowPagerButtons { get; set; } = true;

        /// <summary>
        /// Specifies whether or not the textbox that allows the user to enter the page number
        /// to view is shown. The textbox appears between the pager buttons.
        /// </summary>
        [JsonProperty("pginput")]
        [DefaultValue(true)]
        public bool ShowPagerTextBox { get; set; } = true;

        /// <summary>
        /// The template for the current page status text. The default is "Page {0} of {1}".
        /// </summary>
        [JsonProperty("pgtext")]
        public string PagerNavigationLabelTemplate { get; set; }

        // TODO: prmNames

        /// <summary>
        /// The data appended directly to the // TODO: URL.
        /// </summary>
        [JsonProperty("postData")]
        public object UrlParameters { get; set; }

        /// <summary>
        /// The position of the record information in the pager. The pager element is divided
        /// into 3 positions and only one element can be in a single position.
        /// When changing this option, the <see cref="PagerNavigationAlign"/> // TODO:  must be changed as well.
        /// </summary>
        [JsonProperty("recordpos")]
        [DefaultValue(PagerAlign.Right)]
        public PagerAlign PagerRecordAlign { get; set; } = PagerAlign.Right;

        /// <summary>
        /// The template for the record information text. This text is only displayed if the
        /// number of records is greater than zero. The default is "View {0} - {1} of {2}".
        /// </summary>
        [JsonProperty("recordtext")]
        public string PagerRecordLabelTemplate { get; set; }

        /// <summary>
        /// The two-letter code that correponds to the localization file (grid.locale-xx.js).
        /// The language file must be loaded for this option to work.
        /// </summary>
        // TODO: Make this an enum of allowed language values
        [JsonProperty("regional")]
        [DefaultValue("en")]
        public string Localization { get; set; } = "en";

        // TODO: remapColumns
        // TODO: resizeclass

        /// <summary>
        /// Specifies whether or not the grid is resized automatically to its parent container
        /// when the width of the viewport is changed.
        /// </summary>
        [JsonProperty("responsive")]
        public bool IsResponsive { get; set; }

        /// <summary>
        /// Specifies whether or not the cell should be set or restored to its initial state
        /// on failure.
        /// </summary>
        [JsonProperty("restoreCellonFail")]
        [DefaultValue(true)]
        public bool ShouldRestoreCellOnFailure { get; set; } = true;

        /// <summary>
        /// The list of row count choices in the pager drop-down list. For example, if this option is
        /// set to [10, 20, 50] and 20 is selected, it will set the // TODO: rownum to 20.
        /// </summary>
        [JsonProperty("rowList")]
        public int[] PagerRowOptions { get; set; }

        /// <summary>
        /// Specifies whether or not a new column is shown as the leftmost column in the grid containing
        /// the row number, starting from one. The <see cref="Columns"/> model is automatically extended
        /// with the new column with the reserved name "rn", hence columns in the model should not be named "rn".
        /// </summary>
        [JsonProperty("rownumbers")]
        public bool ShowRowNumberColumn { get; set; }

        /// <summary>
        /// The number of rows to view in the grid. This option is passed to the server when requesting data.
        /// If this option is set to 10 and the server returns 15 rows, only 10 rows will be loaded.
        /// </summary>
        [JsonProperty("rowNum")]
        [DefaultValue(20)]
        public int MaxRows { get; set; } = 20;

        /// <summary>
        /// The total number of rows on which the grid can operate. If specified, an additional parameter
        /// "totalrows" is set to the server
        /// </summary>
        [JsonProperty("rowTotal")]
        public int? TotalRows { get; set; }

        [JsonProperty("rownumWidth")]
        [DefaultValue(35)]
        public int RowNumberColumnWidth { get; set; } = 35;

        // TODO: searchOptions

        /// <summary>
        /// Specifies whether or not to use a dynamic virtual scroll mode.
        /// </summary>
        [JsonProperty("scroll")]
        [DefaultValue(VirtualScrollMode.Disabled)]
        public VirtualScrollMode VirtualScrollMode { get; set; } = VirtualScrollMode.Disabled;

        /// <summary>
        /// The maximum rows the grid can load when the <see cref="ScrollOption"/> is set to <see cref="ScrollOption.VisibleLines"/>.
        /// It is recommended that you set this value greater than <see cref="MaxRows"/>, otherwise it will
        /// default to the <see cref="MaxRows"/> value.
        /// </summary>
        [JsonProperty("scrollMaxBuffer")]
        public int ScrollMaxRows { get; set; }

        // TODO: scrollLeftOffset

        /// <summary>
        /// The width of the vertical scrollbar.
        /// </summary>
        [JsonProperty("scrollOffset")]
        [DefaultValue(18)]
        public int VerticalScrollbarWidth { get; set; } = 18;

        /// <summary>
        /// The top offset from the upper position of the scroll element.
        /// </summary>
        [JsonProperty("scrollTopOffset")]
        public int VerticalScrollbarTopOffset { get; set; }

        /// <summary>
        /// Specifies whether or not a pop-up with page information is displayed when virtual scrolling is enabled.
        /// The pop-up changes its position relative to the position of the scroll element.
        /// </summary>
        [JsonProperty("scrollPopUp")]
        public bool ShowVirtualScrollInfoPopUp { get; set; }

        [JsonProperty("scrollTimeout")]
        [JsonConverter(typeof(TimeSpanConverter))]
        public TimeSpan VirtualScrollTimeout { get; set; } = TimeSpan.FromMilliseconds(40);

        public bool ShouldSerializeVirtualScrollTimeout() => VirtualScrollTimeout != TimeSpan.FromMilliseconds(40);

        /// <summary>
        /// Specifies whether or not the grid is scrolled so that the selected row is visible when
        /// a row is selected via setSelection.
        /// </summary>
        [JsonProperty("scrollrows")]
        public bool ShouldScrollToSelectedRow { get; set; }

        /// <summary>
        /// Specifies whether or not automatic resizing takes place in proportion to each columns' width
        /// to fit within the bounds of the grid's width.
        /// </summary>
        [JsonProperty("shrinkToFit")]
        [DefaultValue(true)]
        public bool ShouldShrinkToFit { get; set; } = true;

        /// <summary>
        /// Specifies whether or not reordering columns by dragging and dropping them with the mouse
        /// is enabled.
        /// </summary>
        [JsonProperty("sortable")]
        public bool CanReorderColumns { get; set; }

        // TODO: sortname

        /// <summary>
        /// The column according to which the data is to be sorted when the grid is initially loaded.
        /// Use in conjunction with <see cref="InitialColumnSortOrder"/>.
        /// </summary>
        [JsonProperty("sortname")]
        public string InitialColumnNameToSort { get; set; }

        /// <summary>
        /// The sort order to use when the grid is initially loaded and sorted by the <see cref="InitialColumnNameToSort"/>.
        /// </summary>
        [JsonProperty("sortorder")]
        [DefaultValue(SortOrder.Ascending)]
        public SortOrder InitialColumnSortOrder { get; set; } = SortOrder.Ascending;

        /// <summary>
        /// Specifies whether or not the navigation options are stored in the grid options when
        /// the grid state is saved or restored.
        /// </summary>
        [JsonProperty("storeNavOptions")]
        public bool ShouldStoreNavigationOptions { get; set; }

        // DefaultValue isn't specified since my default preference is different from jqGrid's default
        [JsonProperty("styleUI")]
        public Style Style { get; set; } = Style.Bootstrap3;

        [JsonProperty("subGrid")]
        public bool HasSubgrid { get; set; }

        // TODO: subGridOptions
        // TODO: subGridModel
        // TODO: subgridtype
        // TODO: subGridUrl
        // TODO: subGridWith
        // TODO: toolbar
        // TODO: toppager
        // TODO: treeGrid
        // TODO: treeGrid_bigData
        // TODO: treeGridModel
        // TODO: treeIcons
        // TODO: treeReader
        // TODO: tree_root_level

        /// <summary>
        /// The URL that returns the data needed to populate the grid.
        /// </summary>
        [JsonProperty("url")]
        public string DataUrl { get; set; }

        // TODO: userDataOnFooter

        /// <summary>
        /// Specifies whether or not the grid performs the search by <see cref="Column.Name"/> instead
        /// of by <see cref="Column.Index"/> when the data type is local.
        /// </summary>
        [JsonProperty("useNameForSearch")]
        public bool UseNameForLocalSearch { get; set; }

        // TODO: viewOptions

        /// <summary>
        /// Specifies whether or not "View X to Y out of Z" is shown in the pager bar.
        /// </summary>
        [JsonProperty("viewrecords")]
        public bool ShowPagerRowCount { get; set; }

        // TODO: viewsortcols

        /// <summary>
        /// The width of the grid, in pixels. If this option is not set, the width of the grid is the sum
        /// of the widths of the columns. If this option is set, the initial width of each column is set
        /// according to the value of the <see cref="ShouldShrinkToFit"/> option.
        /// </summary>
        [JsonProperty("width")]
        public int? Width { get; set; }
        
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

            if (LocalData != null && !(DataType == DataType.XmlLocalData || DataType == DataType.JsonLocalData))
            {
                return false;
            }

            if ((DataType == DataType.XmlLocalData || DataType == DataType.JsonLocalData) && LocalData == null)
            {
                return false;
            }

            if (LocalDataArrayName != null && DataType != DataType.LocalDataArray)
            {
                return false;
            }
            // TODO: If tree grid only, then allow ExpandColumn

            if (Height.Type != UnitType.Pixel || Height.Type != UnitType.Percentage)
            {
                return false;
            }

            if (Height.Type == UnitType.Percentage && Height.Value != 100)
            {
                return false;
            }

            // TODO: IconSet only when Bootstrap4

            if (SelectOnCheckBoxClickOnly && !CanMultiSelect)
            {
                return false;
            }

            if (MultiSelectKey.HasValue && !CanMultiSelect)
            {
                return false;
            }

            // TODO: PagerNavigationAlign and recordpos and otherone should be unique and not set to the same value


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
