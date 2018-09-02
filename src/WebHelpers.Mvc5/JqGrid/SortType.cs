using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WebHelpers.Mvc5.JqGrid
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SortType
    {
        [EnumMember(Value = "text")]
        String,

        [EnumMember(Value = "int")]
        Integer,

        [EnumMember(Value = "number")]
        Decimal,

        [EnumMember(Value = "date")]
        DateTime
    }
}
