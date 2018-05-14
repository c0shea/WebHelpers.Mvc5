using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WebHelpers.Mvc5.JqGrid
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DataDestination
    {
        [EnumMember(Value = "remote")]
        Remote,

        [EnumMember(Value = "clientArray")]
        Local
    }
}
