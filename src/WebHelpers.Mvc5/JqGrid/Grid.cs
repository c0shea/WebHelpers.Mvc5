using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebHelpers.Mvc5.JqGrid
{
    public class Grid
    {
        [JsonProperty("colModel")]
        public List<Column> Columns { get; set; }

        private bool IsValid()
        {
            if (Columns != null)
            {
                var formEditOptions = Columns.Where(c => c.FormEditOptions != null).Select(c => c.FormEditOptions);

                // TODO: Ensure that there are no duplicate row:column position combinations across the columns
            }

            return true;
        }

        public override string ToString()
        {
            var serializer = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            };
            using (var stringWriter = new StringWriter())
            using (var writer = new JsonTextWriter(stringWriter) { QuoteName = false })
            {
                serializer.Serialize(writer, this);

                return stringWriter.ToString();
            }
        }
    }
}
