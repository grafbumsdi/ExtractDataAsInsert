using System;

using ExtractDataAsInsert;
using ExtractDataAsInsert.PlaceholderOptions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExtractDataAsInsertTests
{
    [TestClass()]
    public class PlaceholderTests
    {
        [TestMethod()]
        public void PlaceholderThreeOptions()
        {
            var placeHolder = new Placeholder("{gaxi:DATETIME:RELATIVEDATETIME:RELATIVEDATETIMEUTC}");
            var options = placeHolder.Options;
            Assert.AreEqual(3, options.Count);
            Assert.AreEqual("DATETIME", options[0].GetIdentifier());
            Assert.AreEqual("RELATIVEDATETIME", options[1].GetIdentifier());
            Assert.AreEqual("RELATIVEDATETIMEUTC", options[2].GetIdentifier());
            Assert.IsTrue(placeHolder.IsDateTime);
            Assert.IsTrue(placeHolder.IsRelativeDateTime);
            Assert.IsTrue(placeHolder.IsRelativeDateTimeUtc);
        }

        [TestMethod()]
        public void PlaceholderInvalidOptions()
        {
            Assert.ThrowsException<InvalidOperationException>(
                () => new Placeholder("{gaxi:OPTION1:OPTION2:ODATETIME}"));
        }

        [TestMethod()]
        public void PlaceholderOneOptions()
        {
            var placeHolder = new Placeholder("{gaxi:DATETIME}");
            var options = placeHolder.Options;
            Assert.AreEqual(1, options.Count);
            Assert.AreEqual("DATETIME", options[0].GetIdentifier());
            Assert.IsTrue(placeHolder.IsDateTime);
        }

        [TestMethod()]
        public void PlaceholderNoOptions()
        {
            var placeHolder = new Placeholder("{gaxiDATETIMEOPTION1OPTION2}");
            var options = placeHolder.Options;
            Assert.AreEqual(0, options.Count);
            Assert.IsFalse(placeHolder.IsDateTime);
        }

        [TestMethod()]
        public void PlaceholderOptionsRelativeDateTimeUtc()
        {
            var placeHolder = new Placeholder("{gaxiDATETIMEOPTION1OPTION2:RELATIVEDATETIMEUTC}");
            var options = placeHolder.Options;
            Assert.AreEqual(1, options.Count);
            Assert.IsFalse(placeHolder.IsDateTime);
            Assert.IsFalse(placeHolder.IsRelativeDateTime);
            Assert.IsTrue(placeHolder.IsRelativeDateTimeUtc);
        }

        [TestMethod()]
        public void PlaceholderConstructorWithOption()
        {
            var placeHolder = new Placeholder("Identifier", new DateTimeOption());
            var options = placeHolder.Options;
            Assert.AreEqual(1, options.Count);
            Assert.IsTrue(placeHolder.IsDateTime);
            Assert.IsFalse(placeHolder.IsRelativeDateTime);
            Assert.AreEqual("{Identifier:DATETIME}", placeHolder.ExactPlaceHolderWithBrackets);
            Assert.AreEqual("Identifier", placeHolder.ValueIdentifier);
        }
    }
}