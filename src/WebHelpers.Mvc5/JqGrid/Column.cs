using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebHelpers.Mvc5.JqGrid.ColumnFormatOptions;
using WebHelpers.Mvc5.JqGrid.Converters;

namespace WebHelpers.Mvc5.JqGrid
{
    /// <remarks>
    /// http://www.guriddo.net/documentation/guriddo/javascript/user-guide/basic-grid/#colmodel-options
    /// </remarks>
    public class Column
    {
        // Required
        // ========

        [JsonProperty("name")]
        public string Name { get; set; }

        // Optional
        // ========

        /// <summary>
        /// The alignment of the cell in the data rows. The header cell is not affected.
        /// </summary>
        [JsonProperty("align")]
        [DefaultValue(TextAlign.Left)]
        public TextAlign Align { get; set; } = TextAlign.Left;

        // TODO: cellattr

        /// <summary>
        /// CSS classes to be added to the column. Separate classes with a space (e.g. class1 class2).
        /// To show an ellipsis when the text overflows the cell, specify the <code>ui-ellipsis</code> class.
        /// </summary>
        [JsonProperty("classes")]
        public string Class { get; set; }

        /// <summary>
        /// Enables or disables the column menu in the grid header.
        /// </summary>
        [JsonProperty("colmenu")]
        [DefaultValue(true)]
        public bool IsMenuVisible { get; set; } = true;

        // TODO: coloptions
        // TODO: datefmt

        /// <summary>
        /// Specifies whether or not the field is editable in inline edit and form edit modes.
        /// </summary>
        [JsonProperty("editable")]
        public bool IsEditable { get; set; }

        /// <summary>
        /// The allowed options for the editable column. This property is only valid if
        /// <see cref="IsEditable"/> is true.
        /// </summary>
        /// <example>new { cacheDataUrl = true, delimiter = "|" }</example>
        [JsonProperty("editoptions")]
        public object EditAttributes { get; set; }

        // TODO: editrules

        /// <summary>
        /// The type of input element to create when the cell is in edit mode.
        /// </summary>
        [JsonProperty("edittype")]
        [DefaultValue(EditType.TextBox)]
        public EditType EditType { get; set; } = EditType.TextBox;

        /// <summary>
        /// Specifies whether or not the column should be exported when using the export methods.
        /// Hidden columns are excluded from the export.
        /// </summary>
        [JsonProperty("exportcol")]
        [DefaultValue(true)]
        public bool ShouldExport { get; set; } = true;

        /// <summary>
        /// The sort order that will be used on first sort. Subsequent sorts will toggle, as usual.
        /// </summary>
        [JsonProperty("firstsortorder")]
        [DefaultValue(SortOrder.Ascending)]
        public SortOrder DefaultSortOrder { get; set; } = SortOrder.Ascending;

        /// <summary>
        /// Specifies whether or not recalculation of the width of the column is allowed if the
        /// TODO: ShrinkToFit option is true. The width also doesn't change if the <code>setGridWith</code>
        /// method is used to change the grid's with.
        /// </summary>
        [JsonProperty("fixed")]
        public bool IsFixedWidth { get; set; }

        /// <summary>
        /// Controls how the column is rendered in form edit mode.
        /// </summary>
        [JsonProperty("formoptions")]
        public FormEditOptions FormEditOptions { get; set; }

        /// <summary>
        /// Controls the formatting of the column.
        /// Can be a predefined type (e.g. <see cref="IntegerColumnFormatOptions.Name"/>) or a custom function name.
        /// </summary>
        /// <remarks>
        /// http://www.guriddo.net/documentation/guriddo/javascript/user-guide/formatters/
        /// </remarks>
        [JsonProperty("formatter")]
        [JsonConverter(typeof(FormatterNameConverter))]
        public string FormatterName { get; set; }

        /// <summary>
        /// The name of a function that is called to un-format a cell to get the original value back.
        /// </summary>
        /// <remarks>
        /// http://www.guriddo.net/documentation/guriddo/javascript/user-guide/formatters/#custom-formatter
        /// </remarks>
        [JsonProperty("unformat")]
        [JsonConverter(typeof(FunctionNameConverter))]
        public string UnFormatterName { get; set; }

        /// <summary>
        /// Overrides the default formatting options from the language file for the specified formatter.
        /// </summary>
        [JsonProperty("formatoptions")]
        public ColumnFormatOptions.ColumnFormatOptions FormatOptions { get; set; }

        /// <summary>
        /// Specifies whether or not the column will be frozen once the setFrozenColumns method is called.
        /// </summary>
        [JsonProperty("frozen")]
        public bool IsFrozen { get; set; }

        /// <summary>
        /// Specifies whether or not the column will be hidden from the column chooser and column menu dialogs.
        /// </summary>
        [JsonProperty("hidedlg")]
        public bool IsHiddenInDialogs { get; set; }

        /// <summary>
        /// Specifies whether or not the column is hidden at initialization. If true, the column is not editable
        /// and will not show in the form edit dialog.
        /// </summary>
        [JsonProperty("hidden")]
        public bool IsHidden { get; set; }

        /// <summary>
        /// The index name to use when sorting. If set, this field is used in searching.
        /// </summary>
        [JsonProperty("index")]
        public string Index { get; set; }

        /// <summary>
        /// Defines the JSON mapping for the column.
        /// </summary>
        /// <remarks>
        /// http://www.guriddo.net/documentation/guriddo/javascript/user-guide/basic-grid/#json-data
        /// </remarks>
        [JsonProperty("jsonmap")]
        public string JsonMap { get; set; }

        /// <summary>
        /// Overwrite the defined primary key returned from the server or array data.
        /// Only one column can have this property set to true.
        /// </summary>
        [JsonProperty("key")]
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// The header caption for the column. If this property isn't set, the heading for the column
        /// is the value of the <see cref="Name"/> property.
        /// </summary>
        [JsonProperty("label")]
        public string Label { get; set; }

        



        private bool IsValid()
        {
            /*
             * ShowMenu only valid when if colMenu is true (http://www.guriddo.net/documentation/guriddo/javascript/user-guide/colmenu/)
             *
             */

            if (EditAttributes != null && !IsEditable)
            {
                return false;
            }

            if (ShouldExport && IsHidden)
            {
                return false;
            }

            if (FormatOptions != null)
            {
                if (FormatOptions is IntegerColumnFormatOptions)
                {
                    return IntegerColumnFormatOptions.Name == FormatterName;
                }

                if (FormatOptions is NumberColumnFormatOptions)
                {
                    return NumberColumnFormatOptions.Name == FormatterName;
                }

                if (FormatOptions is CurrencyColumnFormatOptions)
                {
                    return CurrencyColumnFormatOptions.Name == FormatterName;
                }

                if (FormatOptions is DateColumnFormatOptions)
                {
                    return DateColumnFormatOptions.Name == FormatterName;
                }

                if (FormatOptions is EmailColumnFormatOptions)
                {
                    return EmailColumnFormatOptions.Name == FormatterName;
                }

                if (FormatOptions is LinkColumnFormatOptions)
                {
                    return LinkColumnFormatOptions.Name == FormatterName;
                }

                if (FormatOptions is ShowLinkColumnFormatOptions)
                {
                    return ShowLinkColumnFormatOptions.Name == FormatterName;
                }

                if (FormatOptions is CheckBoxColumnFormatOptions)
                {
                    return CheckBoxColumnFormatOptions.Name == FormatterName;
                }

                if (FormatOptions is SelectColumnFormatOptions)
                {
                    return SelectColumnFormatOptions.Name == FormatterName;
                }

                if (FormatOptions is ActionColumnFormatOptions)
                {
                    return ActionColumnFormatOptions.Name == FormatterName;
                }
            }

            if (FormatterName != null)
            {
                // The function should not end with ()
                return !FormatterName.Contains('(') && !FormatterName.Contains(')');
            }

            if (UnFormatterName != null)
            {
                // The function should not end with ()
                return !UnFormatterName.Contains('(') && !UnFormatterName.Contains(')');
            }

            return !string.IsNullOrEmpty(Name);
        }
    }
}
