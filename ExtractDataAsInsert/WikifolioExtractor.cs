using System;
using System.Collections.Generic;
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
            var statementBuilders = new List<IStatementBuilder>()
                                        {
                                            new StatementBuilder.Wikifolio(
                                                this.wikifolioGuid,
                                                this.userGuid),
                                            new StatementBuilder.Url(this.wikifolioGuid),
                                            new StatementBuilder.WikifolioDecisionMakingAssignment(this.wikifolioGuid)
                                        };

            foreach (var statementBuilder in statementBuilders)
            {
                this.WriteInsertsWithStatementBuilder(writer, statementBuilder);
            }
        }

        private void WriteInsertsWithStatementBuilder(TextWriter writer, IStatementBuilder statementBuilder)
        {
            writer.WriteLine($"-- INSERTS FOR: {statementBuilder.Identifier()}");
            this.ReadAndReplace(writer, statementBuilder);
            writer.WriteLine(string.Empty);
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
