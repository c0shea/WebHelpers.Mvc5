using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WebHelpers.Mvc5.JqGrid
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PagerAlign
    {
        [EnumMember(Value = "left")]
        Left,

        [EnumMember(Value = "center")]
        Center,

        [EnumMember(Value = "right")]
        Right
    }
}
