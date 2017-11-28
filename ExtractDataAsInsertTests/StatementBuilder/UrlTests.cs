using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ExtractDataAsInsert.StatementBuilder;

namespace ExtractDataAsInsertTests.StatementBuilder
{
    [TestClass()]
    public class UrlTests
    {
        [TestMethod()]
        public void InsertStatementTest()
        {
            var wikifolioGuid = new Guid();
            var urlStatementBuilder = new ExtractDataAsInsert.StatementBuilder.Url(wikifolioGuid);
            Assert.AreEqual(
                "INSERT INTO [Url]([ID],[Wikifolio],[Url],[Count],[IdentifierType])VALUES({ID},{Wikifolio},{Url},{Count},{IdentifierType})",
                urlStatementBuilder.InsertStatement());
        }

        [TestMethod()]
        public void QueryStatementTest()
        {
            var wikifolioGuid = new Guid("C03CD005-2A25-4A10-A127-903D2135DFB1");
            var urlStatementBuilder = new ExtractDataAsInsert.StatementBuilder.Url(wikifolioGuid);
            Assert.AreEqual(
                "SELECT [ID],[Wikifolio],[Url],[Count],[IdentifierType] FROM dbo.[Url] WHERE  [Wikifolio] = 'c03cd005-2a25-4a10-a127-903d2135dfb1'",
                urlStatementBuilder.QueryStatement());
        }
    }
}