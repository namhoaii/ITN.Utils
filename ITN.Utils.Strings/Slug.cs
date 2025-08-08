using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace ITN.Utils.Strings
{
    internal static class Slug
    {
        public static string Generate(this string input, string separator = "-")
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            string normalized = input.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char c in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            string noDiacritics = sb.ToString().Normalize(NormalizationForm.FormC);

            noDiacritics = noDiacritics
                .Replace("đ", "d")
                .Replace("Đ", "D");

            string noPunctuation = Regex.Replace(noDiacritics, @"[^\w\s]", "");

            string result = Regex.Replace(noPunctuation, @"\s+", separator);

            return result.ToLower();
        }


    }
}
