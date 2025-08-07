using System;
using System.Collections.Generic;
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

        public static string GenerateRandomFileName(string extension)
        {
            //return Guid.NewGuid().ToString() + $".{extension}";  
            return (DateTime.Now.ToString("yyyyMMddHHmmssfff") + $".{extension}");
        }

        public static bool ValidEmail(string email)
        {
            return Regex.IsMatch(email ?? "", @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
        }

        public static string TruncateWithEllipsis(string input, int maxLength)
        {
            if(input.Length <= maxLength)
                return input;

            if (maxLength <= 3)
                return input.Substring(0, maxLength) + "...";

            var firstSpaceIndex = input.IndexOf(' ');
            if (firstSpaceIndex < 0 || firstSpaceIndex >= maxLength)
                return input.Substring(0, maxLength - 3) + "...";

            var truncated = input.Substring(0, maxLength - 3);

            if (input[maxLength-3] == ' ')
                return truncated + "...";

            var lastSpaceIndex = truncated.LastIndexOf(' ');


            if(lastSpaceIndex > 0)
            {
                truncated = truncated.Substring(0, lastSpaceIndex);
            }

            return truncated + "...";

        }

        public static int CountWordsInString(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return 0;

            var words = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return words.Length;
        }
    }
}
