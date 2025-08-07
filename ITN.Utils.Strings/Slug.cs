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
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            string normalized = input.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char c in normalized)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(c);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }
            string noDiacritics = sb.ToString().Normalize(NormalizationForm.FormC);

            string noPunctuation = Regex.Replace(noDiacritics, @"[^\w\s]", "");

            string result = Regex.Replace(noPunctuation, @"\s+", separator);

            return result.ToLower();
        }

    }
}
