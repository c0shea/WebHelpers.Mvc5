using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebHelpers.Mvc5.JqGrid;

namespace WebHelpers.Mvc5.Test
{
    [TestClass]
    public class HelperTests
    {
        [TestMethod]
        public void PascalCaseToLabelOneWord()
        {
            var actual = Helper.PascalCaseToLabel("Test");

            Assert.AreEqual("Test", actual);
        }

        [TestMethod]
        public void PascalCaseToLabelTwoWords()
        {
            var actual = Helper.PascalCaseToLabel("PascalCase");

            Assert.AreEqual("Pascal Case", actual);
        }

        [TestMethod]
        public void PascalCaseToLabelSentence()
        {
            var actual = Helper.PascalCaseToLabel("TheQuickBrownFox");

            Assert.AreEqual("The Quick Brown Fox", actual);
        }

        [TestMethod]
        public void PascalCaseToLabelAcronym()
        {
            var actual = Helper.PascalCaseToLabel("TPR");

            Assert.AreEqual("TPR", actual);
        }

        [TestMethod]
        public void PascalCaseToLabelAcronymMixed()
        {
            var actual = Helper.PascalCaseToLabel("TPRRetail");

            Assert.AreEqual("TPR Retail", actual);
        }
    }
}
