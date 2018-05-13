using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebHelpers.Mvc5.JqGrid;
using WebHelpers.Mvc5.JqGrid.ColumnFormatOptions;

namespace WebHelpers.Mvc5.Test
{
    [TestClass]
    public class JqGridTests
    {
        [TestMethod]
        public void ColumnsWithDefaults()
        {
            var grid = new Grid
            {
                Columns = new List<Column>
                {
                    new Column { Name = "One" },
                    new Column { Name = "Two" },
                    new Column { Name = "Three" }
                }
            };

            var json = grid.ToString();
            var expected = @"{colModel:[{name:""One""},{name:""Two""},{name:""Three""}]}";

            Assert.AreEqual(expected, json);
        }

        [TestMethod]
        public void FormatterNameBuiltIn()
        {
            var grid = new Grid
            {
                Columns = new List<Column>
                {
                    new Column { Name = "One", FormatterName = IntegerColumnFormatOptions.Name }
                }
            };

            var json = grid.ToString();
            var expected = @"{colModel:[{name:""One"",formatter:""integer""}]}";

            Assert.AreEqual(expected, json);
        }

        [TestMethod]
        public void FormatterNameCustom()
        {
            var grid = new Grid
            {
                Columns = new List<Column>
                {
                    new Column { Name = "One", FormatterName = "MyCustomJsFunc" }
                }
            };

            var json = grid.ToString();
            var expected = @"{colModel:[{name:""One"",formatter:MyCustomJsFunc}]}";

            Assert.AreEqual(expected, json);
        }
    }
}
