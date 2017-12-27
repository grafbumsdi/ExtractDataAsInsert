using System;

using NUnit.Framework;

namespace ExtractDataAsInsertTests.StatementBuilder
{
    [TestFixture]
    public class WikifolioDecisionMakingAssignmentTests
    {
        [Test]
        public void InsertStatementTest()
        {
            var wikifolioGuid = default(Guid);
            var wdmaStatementBuilder = new ExtractDataAsInsert.StatementBuilder.WikifolioDecisionMakingAssignment(wikifolioGuid);
            Assert.AreEqual(
                "INSERT INTO [WikifolioDecisionMakingAssignment]([Wikifolio],[DecisionMaking],[CreationDate])VALUES({Wikifolio},{DecisionMaking},{CreationDate:RELATIVEDATETIME})",
                wdmaStatementBuilder.InsertStatement());
        }

        [Test]
        public void QueryStatementTest()
        {
            var wikifolioGuid = new Guid("C03CD005-2A25-4A10-A127-903D2135DFB1");
            var wdmaStatementBuilder = new ExtractDataAsInsert.StatementBuilder.WikifolioDecisionMakingAssignment(wikifolioGuid);
            Assert.AreEqual(
                "SELECT [Wikifolio],[DecisionMaking],[CreationDate] FROM dbo.[WikifolioDecisionMakingAssignment] WHERE  [Wikifolio] = 'c03cd005-2a25-4a10-a127-903d2135dfb1'",
                wdmaStatementBuilder.QueryStatement());
        }
    }
}