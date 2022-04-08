using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebHelpers.Mvc5.JqGrid.ColumnFormatOptions
{
    public sealed class EmailColumnFormatOptions : ColumnFormatOptions
    {
        [JsonIgnore]
        public static string Name => "email";
    }
}
