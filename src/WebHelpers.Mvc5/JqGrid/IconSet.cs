using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
