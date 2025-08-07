using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace ITN.Utils.Strings
{
    public static class StringExtensions
    {
        /// <summary>
        /// Converts the input string into a URL-friendly slug by generating a simplified, lowercase, and hyphen-separated version of the string.
        /// </summary>
        /// <param name="input">The string to be converted into a slug.</param>
        /// <returns>A URL-friendly slug derived from the input string.</returns>
        public static string ToSlug(this string input) => Slug.Generate(input);

        /// <summary>
        /// Converts the input string to title case by capitalizing the first letter of each word.
        /// </summary>
        /// <param name="input">The string to be converted to title case.</param>
        /// <returns>The input string with each word's first letter capitalized.</returns>
        public static string ToTitleCase(this string input) => StringUtilities.CapitalizeFirstLetter(input);

        /// <summary>
        /// Removes diacritics (accent marks) from the input string, returning a version with only non-accented characters.
        /// </summary>
        /// <param name="input">The string from which diacritics will be removed.</param>
        /// <returns>The input string with diacritics removed.</returns>
        public static string RemoveDiacritics(this string input) => Slug.Generate(input, " ");

        /// <summary>
        /// Generates a random file name with the specified extension, defaulting to "txt" if no extension is provided.
        /// </summary>
        /// <param name="extension">The desired file extension (e.g., "txt", "jpg").</param>
        /// <returns>A random file name with the given extension.</returns>
        public static string GenerateFileName(string extension) => StringUtilities.GenerateRandomFileName("txt");

        /// <summary>
        /// Validates whether the given string is a valid email address format.
        /// </summary>
        /// <param name="email">The string to be validated as an email address.</param>
        /// <returns>True if the string is a valid email format, otherwise false.</returns>
        public static bool IsEmailValid(this string email) => StringUtilities.ValidEmail(email);

        /// <summary>
        /// Truncates the string and adds an ellipsis if the string exceeds the maximum length without cutting off words.
        /// If the string is already shorter than or equal to <paramref name="maxLength"/>, the string remains unchanged.
        /// </summary>
        /// <param name="input">The input string to be truncated.</param>
        /// <param name="maxLength">The maximum length of the string after truncation. If the string exceeds this length, an ellipsis will be added to the end of the string.</param>
        /// <returns>A truncated string, with an ellipsis added if necessary.</returns>

        public static string Truncate(this string input, int maxLength) => StringUtilities.TruncateWithEllipsis(input, maxLength);

        /// <summary>
        /// Counts the number of words in the string. A word is defined as a sequence of non-whitespace characters.
        /// The method ignores leading and trailing whitespaces, as well as multiple consecutive spaces between words.
        /// </summary>
        /// <param name="input">The input string to count words from.</param>
        /// <returns>The number of words in the string.</returns>
        public static int CountWords(this string input) => StringUtilities.CountWordsInString(input);
    }
}
