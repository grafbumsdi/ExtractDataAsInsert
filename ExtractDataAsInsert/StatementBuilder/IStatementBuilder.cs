namespace ExtractDataAsInsert.StatementBuilder
{
    public interface IStatementBuilder
    {
        string Identifier();

        string QueryStatement();

        string InsertStatement();
    }
}