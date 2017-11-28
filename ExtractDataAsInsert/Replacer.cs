using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ExtractDataAsInsert
{
    public class Replacer
    {
        private readonly IDictionary<string, object> dictionary;

        private readonly string outputFormatString;

        public Replacer(IDictionary<string, object> dictionary, string outputFormatString)
        {
            this.dictionary = dictionary;
            this.outputFormatString = outputFormatString;
        }

        public string GetFinalOutput()
        {
            var returnString = this.outputFormatString;
            foreach (var placeholder in this.GetPlaceholders())
            {
                var valueForPlaceHolder = this.GetValueForPlaceHolder(placeholder);
                returnString = returnString.Replace(placeholder.ExactPlaceHolderWithBrackets, valueForPlaceHolder);
            }
            return returnString;
        }

        public string GetValueForPlaceHolder(Placeholder placeholder)
        {
            var rawValue = this.dictionary[placeholder.ValueIdentifier];
            if (rawValue == DBNull.Value)
            {
                rawValue = null;
            }
            if (placeholder.IsDateTime)
            {
                rawValue = (rawValue != null) ? ConvertDateToFuckingMsSqlDateTimeFormat((DateTime)rawValue) : null;
            }
            if (placeholder.IsRelativeDateTime)
            {
                return (rawValue != null) ? ConvertToRelativeDateTime((DateTime)rawValue) : "NULL";
            }
            if (placeholder.IsRelativeDateTimeUtc)
            {
                return (rawValue != null) ? ConvertToRelativeDateTimeUtc((DateTime)rawValue) : "NULL";
            }
            if (rawValue == null)
            {
                return "NULL";
            }
            if (rawValue is decimal)
            {
                rawValue = ((decimal)rawValue).ToString(CultureInfo.InvariantCulture);
            }
            return $"'{rawValue.ToString()}'";
        }

        public IList<Placeholder> GetPlaceholders()
        {
            var regexForPlaceholders = new Regex(@"{.*?}");
            var placeHolders = new List<Placeholder>();
            foreach (Match match in regexForPlaceholders.Matches(this.outputFormatString))
            {
                placeHolders.Add(new Placeholder(match.Value));
            }
            return placeHolders;
        }

        private static string ConvertDateToFuckingMsSqlDateTimeFormat(DateTime time)
        {
            // all of this fucking stuff not working
            // return time.ToString("yyyy-MM-dd HH:mm:ss");
            // return time.ToString("O");
            // return time.ToString("u");
            return time.Year + time.Month.ToString("D2") + time.Day.ToString("D2") + " " + time.Hour.ToString("D2")
                   + ":" + time.Minute.ToString("D2") + ":" + time.Second.ToString("D2") + "."
                   + time.Millisecond.ToString("D3");
        }

        private static string ConvertToRelativeDateTime(DateTime time)
        {
            var diff = DateTime.Now.Date - time.Date;
            return
                $"DATEADD(MILLISECOND, {time.TimeOfDay.TotalMilliseconds}, DATEADD(DAY, DATEDIFF(DAY, {diff.TotalDays}, GETDATE()), 0))";
        }

        private static string ConvertToRelativeDateTimeUtc(DateTime timeInUtc)
        {
            var diff = DateTime.UtcNow.Date - timeInUtc.Date;
            return
                $"DATEADD(MILLISECOND, {timeInUtc.TimeOfDay.TotalMilliseconds}, DATEADD(DAY, DATEDIFF(DAY, {diff.TotalDays}, GETUTCDATE()), 0))";
        }
    }
}
