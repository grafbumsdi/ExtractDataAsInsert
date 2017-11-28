namespace ExtractDataAsInsert.PlaceholderOptions
{
    public interface IPlaceholderOption
    {
        string GetIdentifier();

        string RawValueToStringValue(object rawValue);
    }
}