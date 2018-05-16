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
            var expected = @"{colModel:[{name:""One""},{name:""Two""},{name:""Three""}],colNames:[""One"",""Two"",""Three""]}";

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
            var expected = @"{colModel:[{formatter:""integer"",name:""One""}],colNames:[""One""]}";

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
            var expected = @"{colModel:[{formatter:MyCustomJsFunc,name:""One""}],colNames:[""One""]}";

            Assert.AreEqual(expected, json);
        }

        [TestMethod]
        public void ColumnToExpand()
        {
            var grid = new Grid
            {
                Columns = new List<Column>
                {
                    new Column { Name = "One" },
                    new Column { Name = "Two" }
                }
            };
            grid.ColumnToExpand = grid.Columns[1];

            var json = grid.ToString();
            var expected = @"{colModel:[{name:""One""},{name:""Two""}],colNames:[""One"",""Two""],ExpandColumn:""Two""}";

            Assert.AreEqual(expected, json);
        }
    }
}
