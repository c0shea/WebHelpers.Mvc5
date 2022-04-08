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
    /// <remarks>
    /// http://www.guriddo.net/documentation/guriddo/javascript/user-guide/editing/#edittype
    /// </remarks>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EditType
    {
        [EnumMember(Value = "text")]
        TextBox,

        [EnumMember(Value = "textarea")]
        TextArea,

        [EnumMember(Value = "checkbox")]
        CheckBox,

        [EnumMember(Value = "select")]
        DropDownList,

        [EnumMember(Value = "password")]
        Password,

        [EnumMember(Value = "button")]
        Button,

        [EnumMember(Value = "image")]
        Image,

        [EnumMember(Value = "file")]
        File,

        [EnumMember(Value = "custom")]
        Custom
    }
}
