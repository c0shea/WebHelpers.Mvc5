using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
