using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WebHelpers.Mvc5.JqGrid
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum HttpMethod
    {
        [EnumMember(Value = "GET")]
        Get,

        [EnumMember(Value = "POST")]
        Post
    }
}
