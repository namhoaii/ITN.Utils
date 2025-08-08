using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace ITN.Utils.Strings
{
    internal static class StringUtilities
    {
        public static string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            var words = input.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length > 0)
                {
                    words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
                }
            }

            return string.Join(" ", words);
        }

        public static string GenerateRandomFileName(string extension = "txt")
        {
            extension = string.IsNullOrWhiteSpace(extension) ? "txt" : extension;
            return $"{DateTime.Now:yyyyMMddHHmmssfff}.{extension}";
        }

        public static bool ValidEmail(string email)
        {
            return Regex.IsMatch(email ?? "", @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
        }

        public static string TruncateWithEllipsis(this string input, int maxLength, bool keepFullWords = true)
        {
            if (string.IsNullOrEmpty(input) || input.Length <= maxLength)
                return input;

            if (!keepFullWords)
                return input.Substring(0, maxLength) + "...";

            int lastSpace = input.LastIndexOf(' ', maxLength);
            int truncateAt = (lastSpace > 0) ? lastSpace : maxLength;
            return input.Substring(0, truncateAt) + "...";
        }

        public static int CountWordsInString(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return 0;

            return Regex.Matches(input, @"\b[\p{L}\p{N}']+\b").Count;
        }

        public static string EncodeBase64(string input, Encoding encoding = null)
        {
            if (input == null) return null;
            if (input == string.Empty) return string.Empty;

            if (encoding == null)
                encoding = Encoding.UTF8;

            var bytes = encoding.GetBytes(input);
            return Convert.ToBase64String(bytes);
        }

        public static string DecodeBase64(string input, Encoding encoding = null)
        {
            if (input == null) return null;
            if (input == string.Empty) return string.Empty;

            if (encoding == null)
                encoding = Encoding.UTF8;

            try
            {
                var bytes = Convert.FromBase64String(input.Trim());
                return encoding.GetString(bytes);
            }
            catch (FormatException)
            {
                return null;
            }
        }

        public static string RemoveDiacriticsInString(this string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            string normalized = input.Normalize(NormalizationForm.FormD);
            var builder = new StringBuilder();

            foreach (char c in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    builder.Append(c);
                }
            }

            return builder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
