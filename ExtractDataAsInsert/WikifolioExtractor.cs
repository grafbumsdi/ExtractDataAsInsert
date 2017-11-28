using System;
using System.IO;

using ExtractDataAsInsert.StatementBuilder;

namespace ExtractDataAsInsert
{
    public class WikifolioExtractor
    {
        private readonly Guid wikifolioGuid;
        private readonly Guid userGuid;

        public WikifolioExtractor(Guid wikifolioGuid)
        {
            throw new NotImplementedException("wikifolio creation with user creation not implemented yet");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wikifolioGuid"></param>
        /// <param name="existingUserGuid">That will be used for Insert Statements</param>
        public WikifolioExtractor(Guid wikifolioGuid, Guid existingUserGuid)
        {
            this.wikifolioGuid = wikifolioGuid;
            this.userGuid = existingUserGuid;
        }

        public void WriteInserts(TextWriter writer)
        {
            this.WriteWikifolioInserts(writer);
            writer.WriteLine(string.Empty);
            this.WriteUrlInserts(writer);
            writer.WriteLine(string.Empty);
        }

        private void WriteWikifolioInserts(TextWriter writer)
        {
            var wikifolioStatementBuilder = new StatementBuilder.Wikifolio(this.wikifolioGuid, this.userGuid);
            this.ReadAndReplace(writer, wikifolioStatementBuilder);
        }

        private void WriteUrlInserts(TextWriter writer)
        {
            var urlStatementBuilder = new StatementBuilder.Url(this.wikifolioGuid);
            this.ReadAndReplace(writer, urlStatementBuilder);
        }

        private void ReadAndReplace(TextWriter writer, IStatementBuilder statementBuilder)
        {
            foreach (var row in new SqlDataReader().GetRows(statementBuilder))
            {
                writer.WriteLine(new Replacer(row, statementBuilder.InsertStatement()).GetFinalOutput());
            }
        }
    }
}
