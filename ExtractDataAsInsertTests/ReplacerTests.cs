using System;
using System.Collections.Generic;

using ExtractDataAsInsert;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExtractDataAsInsertTests
{
    [TestClass()]
    public class ReplacerTests
    {
        private static readonly DateTime TestDateTime = new DateTime(2017, 11, 1, 14, 13, 02).AddMilliseconds(89);

        private const string FreeText = "free text :option DateTIME anything { $%&$} wer";

        private static readonly Dictionary<string, object> DefaultDictionary =
            new Dictionary<string, object>() { { "gaxi", null }, { "dateNull", null }, { "date", TestDateTime }, { "freeText", FreeText } };

        [TestMethod()]
        public void GetPlaceholdersNoMatches()
        {
            var replacer = new Replacer(DefaultDictionary, "INSERT INTO Wikifolio () VALUES ()");
            Assert.AreEqual(0, replacer.GetPlaceholders().Count);
        }

        [TestMethod()]
        public void GetPlaceholdersOneMatch()
        {
            var replacer = new Replacer(DefaultDictionary, "INSERT INTO Wikifolio () VALUES ({gaxi})");
            var placeHolders = replacer.GetPlaceholders();
            Assert.AreEqual(1, placeHolders.Count);
            Assert.AreEqual("{gaxi}", placeHolders[0].ExactPlaceHolderWithBrackets);
        }

        [TestMethod()]
        public void GetPlaceholdersMultipleMatches()
        {
            var replacer = new Replacer(DefaultDictionary, "INSERT INTO Wikifolio () VALUES ({gaxi},{haxi},{gaxi},{delay},{gaxi})");
            var placeHolders = replacer.GetPlaceholders();
            Assert.AreEqual(5, placeHolders.Count);
            Assert.AreEqual("{haxi}", placeHolders[1].ExactPlaceHolderWithBrackets);
            Assert.AreEqual("{gaxi}", placeHolders[4].ExactPlaceHolderWithBrackets);
        }

        [TestMethod()]
        public void GetPlaceholdersMultipleMatchesWithInvalidOptions()
        {
            var replacer = new Replacer(DefaultDictionary, "INSERT INTO Wikifolio () VALUES ({gaxi:123},{haxi:43},{gaxi},{delay},{gaxi})");
            Assert.ThrowsException<InvalidOperationException>(() => replacer.GetPlaceholders());
        }


        [TestMethod()]
        public void GetFinalOutputOneMatch()
        {
            var replacer = new Replacer(DefaultDictionary, "FreeText: {freeText}");
            Assert.AreEqual($"FreeText: '{FreeText}'", replacer.GetFinalOutput());
        }

        [TestMethod()]
        public void GetFinalOutputOneNullMatch()
        {
            var replacer = new Replacer(DefaultDictionary, "INSERT INTO Wikifolio () VALUES ({gaxi})");
            Assert.AreEqual("INSERT INTO Wikifolio () VALUES (NULL)", replacer.GetFinalOutput());
        }

        [TestMethod()]
        public void GetFinalOutputMultipleMatches()
        {
            var replacer = new Replacer(DefaultDictionary, "INSERT INTO Wikifolio () VALUES ({gaxi}, {gaxi})");
            Assert.AreEqual("INSERT INTO Wikifolio () VALUES (NULL, NULL)", replacer.GetFinalOutput());
        }

        [TestMethod()]
        public void GetFinalOutputDateMatches()
        {
            var replacer = new Replacer(DefaultDictionary, "haha: {date}");
            Assert.AreEqual("haha: '01.11.2017 14:13:02'", replacer.GetFinalOutput());
        }

        [TestMethod()]
        public void GetFinalOutputDateMatchesDateTime()
        {
            var replacer = new Replacer(DefaultDictionary, "SELECT {date:DATETIME}");
            Assert.AreEqual("SELECT '20171101 14:13:02.089'", replacer.GetFinalOutput());
        }

        [TestMethod()]
        public void GetFinalOutputDateMatchesRelativeDateTime()
        {
            var replacer = new Replacer(DefaultDictionary, "SELECT {date:RELATIVEDATETIME}");
            Assert.AreEqual(
                $"SELECT DATEADD(MILLISECOND, 51182089, DATEADD(DAY, DATEDIFF(DAY, {this.DiffToTestTimeInDays}, GETDATE()), 0))",
                replacer.GetFinalOutput());
        }

        [TestMethod()]
        public void GetFinalOutputDateMatchesRelativeDateTimeUtc()
        {
            var replacer = new Replacer(DefaultDictionary, "SELECT {date:RELATIVEDATETIMEUTC}");
            Assert.AreEqual(
                $"SELECT DATEADD(MILLISECOND, 51182089, DATEADD(DAY, DATEDIFF(DAY, {this.DiffToTestTimeInDays}, GETUTCDATE()), 0))",
                replacer.GetFinalOutput());
        }

        private int DiffToTestTimeInDays => (int)(DateTime.Now.Date - TestDateTime.Date).TotalDays;
    }
}