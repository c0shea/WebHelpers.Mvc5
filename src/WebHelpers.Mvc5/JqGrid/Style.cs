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
    public enum Style
    {
        [EnumMember(Value = "jQueryUI")]
        JQueryUi,

        [EnumMember(Value = "Bootstrap")]
        Bootstrap3,

        [EnumMember(Value = "Bootstrap4")]
        Bootstrap4
    }
}
