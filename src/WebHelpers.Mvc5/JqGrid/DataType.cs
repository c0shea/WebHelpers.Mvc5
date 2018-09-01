using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
