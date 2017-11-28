namespace ExtractDataAsInsert.StatementBuilder
{
    public interface IStatementBuilder
    {
        string QueryStatement();

        string InsertStatement();
    }
}