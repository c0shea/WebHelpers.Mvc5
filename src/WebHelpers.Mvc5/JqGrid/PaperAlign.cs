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
