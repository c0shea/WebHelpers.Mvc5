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
    public enum SortType
    {
        [EnumMember(Value = "text")]
        String,

        [EnumMember(Value = "int")]
        Integer,

        [EnumMember(Value = "number")]
        Decimal,

        [EnumMember(Value = "date")]
        DateTime
    }
}
