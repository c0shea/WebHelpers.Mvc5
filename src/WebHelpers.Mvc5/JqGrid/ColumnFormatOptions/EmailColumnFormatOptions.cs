using Newtonsoft.Json;

namespace WebHelpers.Mvc5.JqGrid.ColumnFormatOptions
{
    public sealed class EmailColumnFormatOptions : ColumnFormatOptions
    {
        [JsonIgnore]
        public static string Name => "email";
    }
}
