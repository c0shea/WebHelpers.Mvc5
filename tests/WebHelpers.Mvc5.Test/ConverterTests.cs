using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
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

        private class TimeSpanConverterTest
        {
            [JsonConverter(typeof(TimeSpanConverter))]
            public TimeSpan TimeSpanProperty { get; set; } = TimeSpan.FromMilliseconds(123);
        }
    }
}
