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
    public enum LoadIndicator
    {
        /// <summary>
        /// Disables the jqGrid progress indicator. This allows you to use your own indicator.
        /// </summary>
        [EnumMember(Value = "disable")]
        Disable,

        /// <summary>
        /// Shows the <see cref="Grid.LoadingText"/> in the center of the grid.
        /// </summary>
        [EnumMember(Value = "enable")]
        Enable,

        /// <summary>
        /// Shows the <see cref="Grid.LoadingText"/> in the center of the grid
        /// and blocks all actions in the grid until the AJAX request completes.
        /// </summary>
        [EnumMember(Value = "block")]
        Block
    }
}
