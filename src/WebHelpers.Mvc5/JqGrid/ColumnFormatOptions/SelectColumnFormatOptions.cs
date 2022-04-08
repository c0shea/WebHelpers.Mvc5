using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebHelpers.Mvc5.JqGrid.ColumnFormatOptions
{
    public sealed class SelectColumnFormatOptions : ColumnFormatOptions
    {
        [JsonIgnore]
        public static string Name => "select";

        [JsonProperty("value")]
        public object Value { get; set; }

        /// <summary>
        /// The delimiter used when the <see cref="Value"/> is a string to separate the key-value pairs.
        /// </summary>
        [JsonProperty("delimiter")]
        [DefaultValue(";")]
        public string Delimiter { get; set; } = ";";

        /// <summary>
        /// The separator used to distinguish the keys from the values in the pairs.
        /// </summary>
        [JsonProperty("separator")]
        [DefaultValue(":")]
        public string Separator { get; set; } = ":";
    }
}
