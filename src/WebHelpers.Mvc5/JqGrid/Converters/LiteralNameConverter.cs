using System;
using Newtonsoft.Json;

namespace WebHelpers.Mvc5.JqGrid.Converters
{
    /// <summary>
    /// Outputs the string value as a JavaScript function or variable without quotes in JSON.
    /// e.g. OnEditEventName: "MyJsFuncName" becomes OnEditEventName: MyJsFuncName
    /// </summary>
    public class LiteralNameConverter : JsonConverter
    {
        public override bool CanRead => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue((string)value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanRead is false. The type will skip the converter.");
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}
