using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebHelpers.Mvc5.JqGrid.Converters
{
    /// <summary>
    /// Outputs the string value as a JavaScript function or variable without quotes in JSON.
    /// e.g. VirtualScrollMode: "true" becomes VirtualScrollMode: true
    /// </summary>
    public class LiteralEnumConverter : JsonConverter
    {
        public override bool CanRead => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var e = (System.Enum)value;

            writer.WriteRawValue(GetEnumValueName(e));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanRead is false. The type will skip the converter.");
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(System.Enum);
        }

        private static string GetEnumValueName(System.Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = field.GetCustomAttribute<EnumMemberAttribute>();

            if (attribute == null)
            {
                return value.ToString();
            }

            return attribute.Value;
        }
    }
}
