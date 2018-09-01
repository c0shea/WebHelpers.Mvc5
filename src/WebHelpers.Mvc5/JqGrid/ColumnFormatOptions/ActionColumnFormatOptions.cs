using System.ComponentModel;
using Newtonsoft.Json;
using WebHelpers.Mvc5.JqGrid.Converters;

namespace WebHelpers.Mvc5.JqGrid.ColumnFormatOptions
{
    public sealed class ActionColumnFormatOptions : ColumnFormatOptions
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

        /// <summary>
        /// The name of the function to call after successfully accessing the row for editing prior to allowing
        /// user access to the input fields. The row id is passed to this function.
        /// </summary>
        [JsonProperty("onEdit")]
        [JsonConverter(typeof(LiteralNameConverter))]
        public string OnEditEventName { get; set; }

        /// <summary>
        /// The name of the function to call immediately after the request to save the data to the server is successful.
        /// The data returned from the server is passed to this function. Depending on the server's response, this function
        /// should return true or false.
        /// </summary>
        [JsonProperty("onSuccess")]
        [JsonConverter(typeof(LiteralNameConverter))]
        public string OnSuccessEventName { get; set; }

        /// <summary>
        /// The URL to call to save the data.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// Additional parameters to send to the server.
        /// </summary>
        [JsonProperty("extraparam")]
        public object Parameters { get; set; }

        /// <summary>
        /// The name of the function to call after the data is saved to the server. The row id and the server's response are
        /// passed to this function. The function is called even when the <see cref="Url"/> is set to clientArray.
        /// </summary>
        [JsonProperty("afterSave")]
        [JsonConverter(typeof(LiteralNameConverter))]
        public string AfterSaveEventName { get; set; }

        /// <summary>
        /// The name of the function to call after an AJAX error or the <see cref="OnSuccessEventName"/> returned false.
        /// The row id and the server's response are passed to this function.
        /// The function is called even when the <see cref="Url"/> is set to clientArray.
        /// </summary>
        [JsonProperty("onError")]
        [JsonConverter(typeof(LiteralNameConverter))]
        public string OnErrorEventName { get; set; }

        /// <summary>
        /// The name of the function to call after restoring the row by pressing the Esc key or the cancel button.
        /// The row id is passed to this function.
        /// </summary>
        [JsonProperty("afterRestore")]
        [JsonConverter(typeof(LiteralNameConverter))]
        public string OnEscapeEventName { get; set; }

        /// <summary>
        /// The HTTP method to use when saving data to the server.
        /// </summary>
        [JsonProperty("mtype")]
        [DefaultValue(HttpMethod.Post)]
        public HttpMethod HttpMethod { get; set; } = HttpMethod.Post;

        /// <summary>
        /// The edit grid row options when <see cref="UseFormEditDialog"/> is true.
        /// </summary>
        [JsonProperty("editOptions")]
        // TODO: Split out the valid options into a class instead of generic object.
        public object EditOptions { get; set; }

        /// <summary>
        /// The delete grid row options.
        /// </summary>
        [JsonProperty("delOptions")]
        // TODO: Split out the valid options into a class instead of generic object.
        public object DeleteOptions { get; set; }

        private bool IsValid()
        {
            if (EditOptions != null && !UseFormEditDialog)
            {
                return false;
            }

            return true;
        }
    }
}
