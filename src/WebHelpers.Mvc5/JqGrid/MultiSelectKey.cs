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
