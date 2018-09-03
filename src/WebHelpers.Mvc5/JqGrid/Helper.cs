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

        internal static string PascalCaseToLabel(string input)
        {
            return string.Join(" ", PascalCaseWordPartsRegex.Matches(input)
                                                            .Cast<Match>()
                                                            .Select(match => match.Value));
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
