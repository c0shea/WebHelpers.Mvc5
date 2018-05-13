using System;
using System.Collections.Generic;
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
    }
}
