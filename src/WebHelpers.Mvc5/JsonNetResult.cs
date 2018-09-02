using System;
using System.IO;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace WebHelpers.Mvc5
{
    /// <summary>
    /// Represents a class that is used to send JSON-formatted content to the response using Json.NET.
    /// </summary>
    public class JsonNetResult : ActionResult
    {
        public Encoding ContentEncoding { get; set; }
        public string ContentType { get; set; }
        public object Data { get; set; }

        /// <summary>
        /// Creates a <see cref="JsonNetResult"/> object that serializes the specified object to JSON.
        /// </summary>
        /// <param name="data">The JavaScript object graph to serialize.</param>
        public JsonNetResult(object data)
        {
            Data = data;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var response = context.HttpContext.Response;
            response.ContentType = string.IsNullOrEmpty(ContentType) ? "application/json" : ContentType;

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            if (Data == null)
            {
                return;
            }

            var serializer = JsonSerializer.CreateDefault();

            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, Data);
                response.Write(writer.ToString());
            }
        }
    }
}
