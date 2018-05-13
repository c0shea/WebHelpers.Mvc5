using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebHelpers.Mvc5.JqGrid;

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
    }
}
