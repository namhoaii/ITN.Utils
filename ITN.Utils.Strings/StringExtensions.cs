using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace ITN.Utils.Strings
{
    /// <summary>
    /// A utility class containing string extension methods.
    /// </summary>
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
        public static string RemoveDiacritics(this string input) => StringUtilities.RemoveDiacriticsInString(input);

        /// <summary>
        /// Generates a random file name with the specified extension, defaulting to "txt" if no extension is provided.
        /// </summary>
        /// <param name="extension">The desired file extension (e.g., "txt", "jpg").</param>
        /// <returns>A random file name with the given extension.</returns>
        public static string GenerateFileName(string extension) => StringUtilities.GenerateRandomFileName(extension);

        /// <summary>
        /// Validates whether the given string is a valid email address format.
        /// </summary>
        /// <param name="email">The string to be validated as an email address.</param>
        /// <returns>True if the string is a valid email format, otherwise false.</returns>
        public static bool IsEmailValid(this string email) => StringUtilities.ValidEmail(email);

        /// <summary>
        /// Truncates the string to the specified maximum length and appends an ellipsis if it exceeds that length.
        /// Optionally preserves whole words when truncating.
        /// </summary>
        /// <param name="input">The string to truncate.</param>
        /// <param name="maxLength">The maximum allowed length of the string after truncation.</param>
        /// <param name="keepFullWords">If true, truncation will not split words in half.</param>
        /// <returns>The truncated string, with an ellipsis appended if it was shortened.</returns>
        public static string Truncate(this string input, int maxLength, bool keepFullWords = true) => StringUtilities.TruncateWithEllipsis(input, maxLength);

        /// <summary>
        /// Counts the number of words in the string. A word is defined as a sequence of non-whitespace characters.
        /// The method ignores leading and trailing whitespaces, as well as multiple consecutive spaces between words.
        /// </summary>
        /// <param name="input">The input string to count words from.</param>
        /// <returns>The number of words in the string.</returns>
        public static int CountWords(this string input) => StringUtilities.CountWordsInString(input);

        /// <summary>
        /// Converts the current string to a Base64-encoded string.
        /// </summary>
        /// <param name="input">The string to encode.</param>
        /// <param name="encoding">
        /// The character encoding to use when converting the string to bytes.
        /// If not specified, <see cref="Encoding.UTF8"/> is used by default.
        /// </param>
        /// <returns>
        /// The Base64-encoded representation of the string, or <c>null</c> if <paramref name="input"/> is <c>null</c>.
        /// </returns>
        public static string ToBase64(this string input, Encoding encoding = null) =>StringUtilities.EncodeBase64(input, encoding);

        /// <summary>
        /// Decodes the current Base64-encoded string back to its original string representation.
        /// </summary>
        /// <param name="input">The Base64-encoded string to decode.</param>
        /// <param name="encoding">
        /// The character encoding to use when converting the decoded bytes to a string.
        /// If not specified, <see cref="Encoding.UTF8"/> is used by default.
        /// </param>
        /// <returns>
        /// The decoded string, or <c>null</c> if <paramref name="input"/> is <c>null</c>.
        /// Returns an empty string if the input is empty.
        /// </returns>
        public static string FromBase64(this string input, Encoding encoding = null) => StringUtilities.DecodeBase64(input, encoding);
    }
}
