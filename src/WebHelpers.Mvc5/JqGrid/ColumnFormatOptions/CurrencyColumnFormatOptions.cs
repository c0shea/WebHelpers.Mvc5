using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebHelpers.Mvc5.JqGrid.ColumnFormatOptions
{
    public sealed class CurrencyColumnFormatOptions : NumberColumnFormatOptions
    {
        [JsonIgnore]
        public new static string Name => "currency";

        [JsonProperty("prefix")]
        public string Prefix { get; set; }

        [JsonProperty("suffix")]
        public string Suffix { get; set; }
    }
}
