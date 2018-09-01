using Newtonsoft.Json;

namespace WebHelpers.Mvc5.JqGrid.ColumnFormatOptions
{
    public class NumberColumnFormatOptions : IntegerColumnFormatOptions
    {
        [JsonIgnore]
        public new static string Name => "number";

        [JsonProperty("decimalSeparator")]
        public string DecimalSeparator { get; set; }

        [JsonProperty("decimalPlaces")]
        public int DecimalPlaces { get; set; }
    }
}
