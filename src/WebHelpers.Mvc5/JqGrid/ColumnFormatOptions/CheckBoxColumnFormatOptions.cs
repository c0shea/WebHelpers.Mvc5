using System.ComponentModel;
using Newtonsoft.Json;

namespace WebHelpers.Mvc5.JqGrid.ColumnFormatOptions
{
    public class CheckBoxColumnFormatOptions : ColumnFormatOptions
    {
        [JsonIgnore]
        public static string Name => "checkbox";

        /// <summary>
        /// Specifies whether or not the checkbox can be changed.
        /// </summary>
        [JsonProperty("disabled")]
        [DefaultValue(true)]
        public bool IsDisabled { get; set; } = true;
    }
}
