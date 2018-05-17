using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WebHelpers.Mvc5.JqGrid
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum IconSet
    {
        [EnumMember(Value = "fontAwesome")]
        FontAwesome,

        [EnumMember(Value = "Octicons")]
        Octicons,

        [EnumMember(Value = "Iconic")]
        Iconic
    }
}
