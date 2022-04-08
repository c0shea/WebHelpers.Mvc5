using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
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
            var grid = new Grid("Test")
            {
                ShowPager = false,
                Columns = new List<Column>
                {
                    new Column("One"),
                    new Column("Two"),
                    new Column("Three")
                }
            };

            var json = grid.ToString();
            var expected = @"{colModel:[{name:""One""},{name:""Two""},{name:""Three""}],colNames:[""One"",""Two"",""Three""],datatype:""json"",styleUI:""Bootstrap""}";

            Assert.AreEqual(expected, json);
        }

        [TestMethod]
        public void FormatterNameBuiltIn()
        {
            var grid = new Grid("Test")
            {
                ShowPager = false,
                Columns = new List<Column>
                {
                    new Column("One") { FormatterName = IntegerColumnFormatOptions.Name }
                }
            };

            var json = grid.ToString();
            var expected = @"{colModel:[{formatter:""integer"",name:""One""}],colNames:[""One""],datatype:""json"",styleUI:""Bootstrap""}";

            Assert.AreEqual(expected, json);
        }

        [TestMethod]
        public void FormatterNameCustom()
        {
            var grid = new Grid("Test")
            {
                ShowPager = false,
                Columns = new List<Column>
                {
                    new Column("One") { FormatterName = "MyCustomJsFunc" }
                }
            };

            var json = grid.ToString();
            var expected = @"{colModel:[{formatter:MyCustomJsFunc,name:""One""}],colNames:[""One""],datatype:""json"",styleUI:""Bootstrap""}";

            Assert.AreEqual(expected, json);
        }

        [TestMethod]
        public void ColumnToExpand()
        {
            var grid = new Grid("Test")
            {
                ShowPager = false,
                Columns = new List<Column>
                {
                    new Column("One"),
                    new Column("Two")
                }
            };
            grid.ColumnToExpand = grid.Columns[1];

            var json = grid.ToString();
            var expected = @"{colModel:[{name:""One""},{name:""Two""}],colNames:[""One"",""Two""],datatype:""json"",ExpandColumn:""Two"",styleUI:""Bootstrap""}";

            Assert.AreEqual(expected, json);
        }

        [TestMethod]
        public void VirtualScrollMode()
        {
            var grid = new Grid("Test")
            {
                ShowPager = false,
                VirtualScrollMode = JqGrid.VirtualScrollMode.Infinite
            };

            var json = grid.ToString();
            var infinite = @"{datatype:""json"",scroll:true,styleUI:""Bootstrap""}";

            Assert.AreEqual(infinite, json);

            grid.VirtualScrollMode = JqGrid.VirtualScrollMode.OnlyVisibleRows;
            json = grid.ToString();
            var onlyVisibleRows = @"{datatype:""json"",scroll:1,styleUI:""Bootstrap""}";

            Assert.AreEqual(onlyVisibleRows, json);
        }
    }
}
