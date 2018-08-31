using System.Runtime.Serialization;
using Newtonsoft.Json;
using WebHelpers.Mvc5.JqGrid.Converters;

namespace WebHelpers.Mvc5.JqGrid
{
    [JsonConverter(typeof(LiteralEnumConverter))]
    public enum VirtualScrollMode
    {
        [EnumMember(Value = "false")]
        Disabled,

        /// <summary>
        /// The pager buttons and select box are disabled and the vertical scrollbar is used
        /// to load data. The grid will always hold all the rows from the start to the latest
        /// point visited.
        /// </summary>
        [EnumMember(Value = "true")]
        Infinite,

        /// <summary>
        /// The grid will only load the visible rows without having to care about memory leaks.
        /// </summary>
        [EnumMember(Value = "1")]
        OnlyVisibleRows
    }
}
