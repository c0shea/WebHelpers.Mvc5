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
    public enum DataType
    {
        [EnumMember(Value = "xml")]
        Xml,

        /// <summary>
        /// Use along with <see cref="Grid.LocalData"/>.
        /// </summary>
        [EnumMember(Value = "xmlstring")]
        XmlLocalData,

        [EnumMember(Value = "json")]
        Json,

        /// <summary>
        /// Use along with <see cref="Grid.LocalData"/>
        /// </summary>
        [EnumMember(Value = "jsonstring")]
        JsonLocalData,

        /// <summary>
        /// Use along with <see cref="Grid.LocalDataArrayName"/>.
        /// </summary>
        [EnumMember(Value = "local")]
        LocalDataArray,

        [EnumMember(Value = "script")]
        JavaScript,

        [EnumMember(Value = "function")]
        CustomFunction,

        [EnumMember(Value = "JSONP")]
        JsonP
    }
}
