using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebHelpers.Mvc5.JqGrid
{
    internal static class Helper
    {
        // Credit: Humanizer (https://github.com/Humanizr/Humanizer/blob/master/src/Humanizer/StringHumanizeExtensions.cs#L16)
        private static readonly Regex PascalCaseWordPartsRegex = new Regex(
            @"[\p{Lu}]?[\p{Ll}]+|[0-9]+[\p{Ll}]*|[\p{Lu}]+(?=[\p{Lu}][\p{Ll}]|[0-9]|\b)|[\p{Lo}]+",
            RegexOptions.IgnorePatternWhitespace | RegexOptions.ExplicitCapture | RegexOptions.Compiled
        );

        internal static string FromPascalCase(string input)
        {
            var result = string.Join(" ", PascalCaseWordPartsRegex
                .Matches(input).Cast<Match>()
                .Select(match => match.Value.ToCharArray().All(char.IsUpper) &&
                                 (match.Value.Length > 1 || (match.Index > 0 && input[match.Index - 1] == ' ') || match.Value == "I")
                    ? match.Value
                    : match.Value.ToLower()));

            return result.Length > 0 ? char.ToUpper(result[0]) +
                                       result.Substring(1, result.Length - 1) : result;
        }

        internal static SortType MapSortType<TProperty>()
        {
            switch (Type.GetTypeCode(typeof(TProperty)))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                    return SortType.Integer;

                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return SortType.Decimal;

                case TypeCode.DateTime:
                    return SortType.DateTime;

                default:
                    return SortType.String;
            }
        }
    }
}
