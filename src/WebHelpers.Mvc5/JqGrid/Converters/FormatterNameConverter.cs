using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebHelpers.Mvc5.JqGrid.ColumnFormatOptions;

namespace WebHelpers.Mvc5.JqGrid.Converters
{
    public class FormatterNameConverter : LiteralNameConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var stringValue = (string)value;

            if (stringValue == IntegerColumnFormatOptions.Name ||
                stringValue == NumberColumnFormatOptions.Name ||
                stringValue == CurrencyColumnFormatOptions.Name ||
                stringValue == DateColumnFormatOptions.Name ||
                stringValue == EmailColumnFormatOptions.Name ||
                stringValue == LinkColumnFormatOptions.Name ||
                stringValue == ShowLinkColumnFormatOptions.Name ||
                stringValue == CheckBoxColumnFormatOptions.Name ||
                stringValue == SelectColumnFormatOptions.Name ||
                stringValue == ActionColumnFormatOptions.Name)
            {
                writer.WriteValue(stringValue);
            }
            else
            {
                base.WriteJson(writer, value, serializer);
            }
        }
    }
}
