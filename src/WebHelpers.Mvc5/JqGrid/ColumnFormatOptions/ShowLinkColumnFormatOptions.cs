using System.ComponentModel;
using Newtonsoft.Json;

namespace WebHelpers.Mvc5.JqGrid.ColumnFormatOptions
{
    public class ShowLinkColumnFormatOptions : LinkColumnFormatOptions
    {
        [JsonIgnore]
        public new static string Name => "showLink";

        /// <summary>
        /// The base URL of the link, e.g. "http://myserver.com/"
        /// </summary>
        [JsonProperty("baseLinkUrl")]
        public string BaseUrl { get; set; }

        /// <summary>
        /// The value that is added after the <see cref="BaseUrl"/>, e.g. "Edit"
        /// </summary>
        [JsonProperty("showaction")]
        public string Action { get; set; }

        /// <summary>
        /// Additional query string parameters, e.g. "&amp;status=change"
        /// </summary>
        [JsonProperty("addParam")]
        public string Parameters { get; set; }

        /// <summary>
        /// The first parameter that is added after the <see cref="Action"/>.
        /// The default is the row id.
        /// </summary>
        [JsonProperty("idName")]
        [DefaultValue("id")]
        public string FirstParameter { get; set; } = "id";
    }
}
