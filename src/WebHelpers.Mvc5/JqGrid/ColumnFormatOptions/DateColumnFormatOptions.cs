using Newtonsoft.Json;

namespace WebHelpers.Mvc5.JqGrid.ColumnFormatOptions
{
    public sealed class DateColumnFormatOptions : ColumnFormatOptions
    {
        [JsonIgnore]
        public static string Name => "date";

        /// <summary>
        /// The format of the date that should be converted.
        /// </summary>
        [JsonProperty("srcformat")]
        public string SourceFormat { get; set; }

        /// <summary>
        /// The new output format.
        /// </summary>
        [JsonProperty("newformat")]
        public string OutputFormat { get; set; }

        /// <summary>
        /// Regular expression for parsing date strings.
        /// </summary>
        [JsonProperty("parseRe")]
        public string ParseRegex { get; set; }

        /// <summary>
        /// Specifies whether or not the date should be reformatted after edited
        /// (i.e. after the user changes the date and saves it to the grid).
        /// </summary>
        [JsonProperty("reformatAfterEdit")]
        public bool ShouldReformatAfterEdit { get; set; }

        /// <summary>
        /// Specifies whether or not the local time offset should be calculated and
        /// included in the date when the date is inserted into the grid.
        /// </summary>
        [JsonProperty("userLocalTime")]
        public bool IncludeLocalTimeOffset { get; set; }
    }
}
