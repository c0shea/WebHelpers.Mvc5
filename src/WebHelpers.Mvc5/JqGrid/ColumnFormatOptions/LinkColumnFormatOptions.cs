using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebHelpers.Mvc5.JqGrid.ColumnFormatOptions
{
    public class LinkColumnFormatOptions : ColumnFormatOptions
    {
        [JsonIgnore]
        public static string Name => "link";

        /// <summary>
        /// Specifies where to open the linked document.
        /// See <see cref="LinkTarget"/> for built-in browser targets, or specify a custom named frame.
        /// </summary>
        [JsonProperty("target")]
        public string Target { get; set; }
    }
}
