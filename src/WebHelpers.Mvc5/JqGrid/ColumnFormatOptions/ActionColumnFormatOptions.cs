using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebHelpers.Mvc5.JqGrid.ColumnFormatOptions
{
    public class ActionColumnFormatOptions : ColumnFormatOptions
    {
        [JsonIgnore]
        public static string Name => "actions";

        /// <summary>
        /// Specifies whether or not pressing the Esc key exits edit mode without saving and pressing
        /// the Enter key saves the current field. If the field is a textarea, pressing enter will not save
        /// the field.
        /// </summary>
        [JsonProperty("keys")]
        public bool IsKeyBindingEnabled { get; set; }

        [JsonProperty("editbutton")]
        [DefaultValue(true)]
        public bool IsEditButtonEnabled { get; set; } = true;

        [JsonProperty("delbutton")]
        [DefaultValue(true)]
        public bool IsDeleteButtonEnabled { get; set; } = true;

        /// <summary>
        /// Specifies whether or not the for edit dialog is used instead of the inline form.
        /// </summary>
        [JsonProperty("editformbutton")]
        public bool UseFormEditDialog { get; set; }

        // TODO: Finish adding events
    }
}
