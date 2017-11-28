using System;
using System.IO;

using ExtractDataAsInsert;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExtractDataAsInsertTests
{
    [TestClass()]
    public class WikifolioExtractorTests
    {
        private const string ResultString = @"-- INSERTS FOR: Wikifolio
INSERT INTO [dbo].[Wikifolio]
           ([ID]
           ,[Isin]
           ,[Wkn]
           ,[NamePrefix]
           ,[Name]
           ,[NamePostfix]
           ,[ShortDescription]
           ,[LongDescription]
           ,[Keywords]
           ,[RatioContracts]
           ,[ContractsSold]
           ,[CashAccountCurrentBalance]
           ,[CashAccountStartBalance]
           ,[Editor]
           ,[RealMoneyInvestor]
           ,[Status]
           ,[BlockedFromEmissionProcess]
           ,[DailyFeeRelative]
           ,[PerformanceFeeRelative]
           ,[TradingReminderEmailToBeSent]
           ,[TestPeriodeOverEmailToBeSent]
           ,[MaxTickInterval]
           ,[MinTickInterval]
           ,[Url]
           ,[TransmitTickToCoreInterval]
           ,[MarkupRelative]
           ,[MarkdownRelative]
           ,[Watermark]
           ,[TermSheet]
           ,[RealMoneyInvestorSetDate]
           ,[BlockedFromEmissionProcessReason]
           ,[ContainsLeverageProducts]
           ,[WikifolioType]
           ,[CreationDate]
           ,[PublishingDate]
           ,[PublishingPrice]
           ,[EmissionDate]
           ,[EmissionPrice]
           ,[SavingPlan]
           ,[ContractsSoldDistinct]
           ,[NoIndex]
           ,[DefaultLanguage]
           ,[Currency])VALUES('01db9db8-dc41-4fef-8172-001f101573f9',NULL,NULL,'WF','YNGSTR','LS','youngster','Ein gemischtes Depot aus kurzfristigen Chancen und konservativen Ansätzen. Auswahl erfolgt fundamental, sowie technisch. Das Bauchgefühl hat durchaus auch seinen Beteiligungsfaktor.

Erfahrungen habe ich durch mein Wirtschaftsstudium erlangt, sowie durch privates Engagement im Finanzbereich. Interesse und Neugier haben dabei eine große Rolle gespielt. 

Derzeit befinde ich mich im Praktikum für einen privaten Vermögensverwalter und versuche dort mein Wissen zu vertiefen.

Mit meiner Strategie möchte ich mir etwas zum Studium dazu verdienen.

http://www.linkedin.com/pub/david-lux/48/2a3/99',NULL,'0.001000000000','0.000000000000','8586.530524890375','100000.000000000000','b6ab34aa-9587-4f1e-a54c-2dcf23764fce','False','160','True','0.950000000000','5.000000000000','False','True','900','1','youngster','900','0.150000000000','0.150000000000','116.240206263423',NULL,NULL,NULL,'False','1',DATEADD(MILLISECOND, 62173357, DATEADD(DAY, DATEDIFF(DAY, 1462, GETDATE()), 0)),DATEADD(MILLISECOND, 37978507, DATEADD(DAY, DATEDIFF(DAY, 1461, GETDATE()), 0)),'100.158614898750',NULL,NULL,'False','0.000000000000',NULL,'DE','EUR')

-- INSERTS FOR: Url
INSERT INTO [Url]([ID],[Wikifolio],[Url],[Count],[IdentifierType])VALUES('263aae9a-aa81-44f7-9961-04049daf4f7d','01db9db8-dc41-4fef-8172-001f101573f9','YNGSTR','1','NAME')
INSERT INTO [Url]([ID],[Wikifolio],[Url],[Count],[IdentifierType])VALUES('e22684c4-e11c-4ed3-8d07-592f7cd42de3','01db9db8-dc41-4fef-8172-001f101573f9','WF00YNGSTR','1','FULLNAME')
INSERT INTO [Url]([ID],[Wikifolio],[Url],[Count],[IdentifierType])VALUES('c03cd005-2a25-4a10-a127-903d2135dfb1','01db9db8-dc41-4fef-8172-001f101573f9','youngster','1','URL')
INSERT INTO [Url]([ID],[Wikifolio],[Url],[Count],[IdentifierType])VALUES('95719aaf-53c8-4b62-b5e4-ff561ebed22a','01db9db8-dc41-4fef-8172-001f101573f9','YNGSTR-youngster','1','URL')

-- INSERTS FOR: WikifolioDecisionMakingAssignment

";
        [TestMethod()]
        public void WriteInsertsTest()
        {
            var wikifolioGuid = new Guid("01DB9DB8-DC41-4FEF-8172-001F101573F9");
            var userGuid = new Guid("B6AB34AA-9587-4F1E-A54C-2DCF23764FCE");
            var wikifolioExtractor = new WikifolioExtractor(wikifolioGuid, userGuid);
            var stringWriter = new StringWriter();
            wikifolioExtractor.WriteInserts(stringWriter);
            Assert.AreEqual(ResultString, stringWriter.ToString());
        }
    }
}