using System;

namespace ExtractDataAsInsert
{
    class Program
    {
        static void Main(string[] args)
        {
            var wikifolioGuid = new Guid("01DB9DB8-DC41-4FEF-8172-001F101573F9");
            var userGuid = new Guid("B6AB34AA-9587-4F1E-A54C-2DCF23764FCE");
            new WikifolioExtractor(wikifolioGuid, userGuid).WriteInserts(Console.Out);
        }
    }
}
