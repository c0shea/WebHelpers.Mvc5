using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WebHelpers.Mvc5.JqGrid
{
    /// <remarks>
    /// http://www.guriddo.net/documentation/guriddo/javascript/user-guide/searching/#configuration
    /// </remarks>>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SearchType
    {
        [EnumMember(Value = "text")]
        Text,

        [EnumMember(Value = "select")]
        Select,

        [EnumMember(Value = "custom")]
        Custom
    }
}
