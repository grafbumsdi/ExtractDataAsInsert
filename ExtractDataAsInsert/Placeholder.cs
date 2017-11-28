using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ExtractDataAsInsert
{
    public class Placeholder
    {
        private const string OptionsDelimiter = ":";

        private const string DateTimeOption = "DATETIME";

        private const string DateTimeRelativeOption = "RELATIVEDATETIME";

        private const string DateTimeRelativeUtcOption = "RELATIVEDATETIMEUTC";

        public Placeholder(string placeHolderStringWithBrackets)
        {
            this.ExactPlaceHolderWithBrackets = placeHolderStringWithBrackets;

            var placeHolderWithoutBrackets = RemoveBrackets(placeHolderStringWithBrackets);

            this.Options = GetOptions(placeHolderWithoutBrackets);

            if (this.Options.Count > 0)
            {
                var positionOfDelimiter = placeHolderWithoutBrackets.IndexOf($"{OptionsDelimiter}", StringComparison.Ordinal);
                this.ValueIdentifier = placeHolderWithoutBrackets.Substring(0, positionOfDelimiter);
            }
            else
            {
                this.ValueIdentifier = placeHolderWithoutBrackets;
            }
        }

        public string ExactPlaceHolderWithBrackets { get; private set; }

        public string ValueIdentifier { get; private set; }

        public IList<string> Options { get; private set; }

        public bool IsDateTime => this.Options.Contains(DateTimeOption);

        public bool IsRelativeDateTime => this.Options.Contains(DateTimeRelativeOption);

        public bool IsRelativeDateTimeUtc => this.Options.Contains(DateTimeRelativeUtcOption);

        private static string RemoveBrackets(string placeholderWithBrackets)
        {
            if (!placeholderWithBrackets.StartsWith("{"))
            {
                throw new Exception("Placeholder did not start with {. But: '" + placeholderWithBrackets + "'");
            }
            if (!placeholderWithBrackets.EndsWith("}"))
            {
                throw new Exception("Placeholder did not end with }. But: '" + placeholderWithBrackets + "'");
            }
            if (placeholderWithBrackets.LastIndexOf("{", StringComparison.Ordinal) > 0)
            {
                throw new Exception("Multiple occurences of { . Input was: '" + placeholderWithBrackets + "'");
            }
            if (placeholderWithBrackets.IndexOf("}", StringComparison.Ordinal) < placeholderWithBrackets.Length - 1)
            {
                throw new Exception("Multiple occurences of } . Input was: '" + placeholderWithBrackets + "'");
            }
            return placeholderWithBrackets.Replace("{", string.Empty).Replace("}", string.Empty);
        }

        private static IList<string> GetOptions(string placeholder)
        {
            var optionsRegex = new Regex($"{OptionsDelimiter}[^{OptionsDelimiter}]+");
            var options = new List<string>();
            foreach (Match optionMatch in optionsRegex.Matches(placeholder))
            {
                options.Add(optionMatch.Value.Substring(OptionsDelimiter.Length));
            }
            return options;
        }
    }
}
