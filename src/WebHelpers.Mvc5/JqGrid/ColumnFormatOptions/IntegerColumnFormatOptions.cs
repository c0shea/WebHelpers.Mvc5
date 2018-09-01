using Newtonsoft.Json;

namespace WebHelpers.Mvc5.JqGrid.ColumnFormatOptions
{
    public class IntegerColumnFormatOptions : ColumnFormatOptions
    {
        [JsonIgnore]
        public static string Name => "integer";

        [JsonProperty("thousandsSeparator")]
        public string ThousandsSeparator { get; set; }

        [JsonProperty("defaultValue")]
        public string DefaultValue { get; set; }
    }
}
