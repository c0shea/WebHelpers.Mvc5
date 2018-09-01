using Newtonsoft.Json;

namespace WebHelpers.Mvc5.JqGrid
{
    public class FormEditOptions
    {
        /// <summary>
        /// The ordinal column position of the element in the form, starting from 1.
        /// </summary>
        [JsonProperty("colpos")]
        public int ColumnPosition { get; set; }

        /// <summary>
        /// Text or HTML content to show before the input element.
        /// </summary>
        [JsonProperty("elmprefix")]
        public string InputPrefix { get; set; }

        /// <summary>
        /// Text or HTML content to show after the input element.
        /// </summary>
        [JsonProperty("elmsuffix")]
        public string InputSuffix { get; set; }

        /// <summary>
        /// Replaces the name of the column displayed in the form.
        /// </summary>
        [JsonProperty("label")]
        public string Label { get; set; }

        /// <summary>
        /// Enables adding the text specified in <see cref="TextAboveInput"/> above the input element.
        /// </summary>
        [JsonProperty("rowabove")]
        public bool ShowTextAboveInput { get; set; }

        /// <summary>
        /// The text the appears above the input element. Only shown if <see cref="ShowTextAboveInput"/> is true.
        /// </summary>
        [JsonProperty("rowcontent")]
        public string TextAboveInput { get; set; }

        /// <summary>
        /// The ordinal row position of the element in the form, starting from 1.
        /// </summary>
        [JsonProperty("rowpos")]
        public int RowPosition { get; set; }

        private bool IsValid()
        {
            if (!string.IsNullOrEmpty(TextAboveInput) && !ShowTextAboveInput)
            {
                return false;
            }

            return true;
        }
    }
}
