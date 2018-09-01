using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WebHelpers.Mvc5.JqGrid
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SortOrder
    {
        [EnumMember(Value = "asc")]
        Ascending,

        [EnumMember(Value = "desc")]
        Descending
    }
}
