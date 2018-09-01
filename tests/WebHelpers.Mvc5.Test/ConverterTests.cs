using System;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using WebHelpers.Mvc5.JqGrid.ColumnFormatOptions;
using WebHelpers.Mvc5.JqGrid.Converters;

namespace WebHelpers.Mvc5.Test
{
    [TestClass]
    public class ConverterTests
    {
        [TestMethod]
        public void TimeSpanConverter()
        {
            var actual = JsonConvert.SerializeObject(new TimeSpanConverterTest());
            var expected = @"{""TimeSpanProperty"":123}";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LiteralNameConverter()
        {
            var actual = JsonConvert.SerializeObject(new LiteralNameConverterTest());
            var expected = @"{""LiteralNameProperty"":JsMethod}";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LiteralEnumConverter()
        {
            var actual = JsonConvert.SerializeObject(new LiteralEnumConverterTest());
            var expected = @"{""One"":1,""Two"":Second,""Three"":Three}";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FormatterNameConverter()
        {
            var actual = JsonConvert.SerializeObject(new FormatterNameConverterTest());
            var expected = @"{""BuiltInFormatter"":""integer"",""CustomFormatter"":customFormatMethod}";

            Assert.AreEqual(expected, actual);
        }

        private class TimeSpanConverterTest
        {
            [JsonConverter(typeof(TimeSpanConverter))]
            public TimeSpan TimeSpanProperty { get; set; } = TimeSpan.FromMilliseconds(123);
        }

        private class LiteralNameConverterTest
        {
            [JsonConverter(typeof(LiteralNameConverter))]
            public string LiteralNameProperty { get; set; } = "JsMethod";
        }

        private class LiteralEnumConverterTest
        {
            public LiteralEnum One { get; set; } = LiteralEnum.One;
            public LiteralEnum Two { get; set; } = LiteralEnum.Two;
            public LiteralEnum Three { get; set; } = LiteralEnum.Three;
        }

        [JsonConverter(typeof(LiteralEnumConverter))]
        private enum LiteralEnum
        {
            [EnumMember(Value = "1")]
            One,

            [EnumMember(Value = "Second")]
            Two,
            
            Three
        }

        private class FormatterNameConverterTest
        {
            [JsonConverter(typeof(FormatterNameConverter))]
            public string BuiltInFormatter { get; set; } = IntegerColumnFormatOptions.Name;

            [JsonConverter(typeof(FormatterNameConverter))]
            public string CustomFormatter { get; set; } = "customFormatMethod";
        }
    }
}
