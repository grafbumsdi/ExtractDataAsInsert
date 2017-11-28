using ExtractDataAsInsert;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExtractDataAsInsertTests
{
    [TestClass()]
    public class PlaceholderTests
    {
        [TestMethod()]
        public void PlaceholderTwoOptionsTest()
        {
            var placeHolder = new Placeholder("{gaxi:OPTION1:OPTION2:ODATETIME}");
            var options = placeHolder.Options;
            Assert.AreEqual(3, options.Count);
            Assert.AreEqual("OPTION1", options[0]);
            Assert.AreEqual("OPTION2", options[1]);
            Assert.AreEqual("ODATETIME", options[2]);
            Assert.IsFalse(placeHolder.IsDateTime);
        }

        [TestMethod()]
        public void PlaceholderOneOptionsTest()
        {
            var placeHolder = new Placeholder("{gaxi:DATETIME}");
            var options = placeHolder.Options;
            Assert.AreEqual(1, options.Count);
            Assert.AreEqual("DATETIME", options[0]);
            Assert.IsTrue(placeHolder.IsDateTime);
        }

        [TestMethod()]
        public void PlaceholderNoOptionsTest()
        {
            var placeHolder = new Placeholder("{gaxiDATETIMEOPTION1OPTION2}");
            var options = placeHolder.Options;
            Assert.AreEqual(0, options.Count);
            Assert.IsFalse(placeHolder.IsDateTime);
        }

        [TestMethod()]
        public void PlaceholderOptionsRelativeDateTimeUtcTest()
        {
            var placeHolder = new Placeholder("{gaxiDATETIMEOPTION1OPTION2:RELATIVEDATETIMEUTC}");
            var options = placeHolder.Options;
            Assert.AreEqual(1, options.Count);
            Assert.IsFalse(placeHolder.IsDateTime);
            Assert.IsFalse(placeHolder.IsRelativeDateTime);
            Assert.IsTrue(placeHolder.IsRelativeDateTimeUtc);
        }
    }
}