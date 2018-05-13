using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebHelpers.Mvc5.JqGrid.ColumnFormatOptions
{
    public class EmailColumnFormatOptions : ColumnFormatOptions
    {
        [JsonIgnore]
        public static string Name => "email";
    }
}
