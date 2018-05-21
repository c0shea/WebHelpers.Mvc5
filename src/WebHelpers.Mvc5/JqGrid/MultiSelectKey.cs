using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WebHelpers.Mvc5.JqGrid
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MultiSelectKey
    {
        [EnumMember(Value = "shiftKey")]
        Shift,

        [EnumMember(Value = "altKey")]
        Alt,

        [EnumMember(Value = "ctrlKey")]
        Ctrl
    }
}
