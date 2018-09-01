using Newtonsoft.Json;

namespace WebHelpers.Mvc5.JqGrid.ColumnFormatOptions
{
    public class CurrencyColumnFormatOptions : NumberColumnFormatOptions
    {
        [JsonIgnore]
        public new static string Name => "currency";

        [JsonProperty("prefix")]
        public string Prefix { get; set; }

        [JsonProperty("suffix")]
        public string Suffix { get; set; }
    }
}
