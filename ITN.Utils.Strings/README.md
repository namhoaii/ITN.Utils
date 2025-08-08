# StringExtensions for C#

A lightweight and handy collection of C# extension methods for working with strings.  
This library provides commonly used string utilities such as slug generation, title casing, email validation, diacritic removal, Base64 encoding/decoding, and more.

## ✨ Features

- ✅ Convert strings to URL-friendly slugs
- ✅ Capitalize the first letter of each word (title case)
- ✅ Remove diacritics (accents) from characters
- ✅ Validate email addresses
- ✅ Generate random file names
- ✅ Truncate strings with ellipsis (optionally keep full words)
- ✅ Count words in a string
- ✅ Encode / Decode Base64

## 📦 Installation

```bash
dotnet add package ITN.Utils.Strings
```

_or_

```PM
Install-Package ITN.Utils.Strings
```

## 🚀 Usage

```csharp
using ITN.Utils.Strings;

class Example
{
    void Run()
    {
        string title = "Hello world from CSharp";

        string slug = title.ToSlug();
        // hello-world-from-csharp

        string titleCase = title.ToTitleCase();
        // Hello World From Csharp

        string noDiacritics = "Crème brûlée".RemoveDiacritics();
        // Creme brulee

        bool isEmailValid = "example@email.com".IsEmailValid();
        // true

        string fileName = StringExtensions.GenerateFileName("jpg");
        // e.g., 20250807105626143.jpg

        string shortText = "This is a very long sentence".Truncate(10);
        // "This is a..."

        int wordCount = " Count   these words ".CountWords();
        // 3

        string encoded = "Hello".ToBase64();
        // SGVsbG8=

        string decoded = "SGVsbG8=".FromBase64();
        // Hello
    }
}
```

## 📚 Method Reference

| Method                                                           | Description                                                               |
| ---------------------------------------------------------------- | ------------------------------------------------------------------------- |
| `ToSlug(this string input)`                                      | Converts string to URL-friendly slug (lowercase, hyphen-separated)        |
| `ToTitleCase(this string input)`                                 | Capitalizes the first letter of each word                                 |
| `RemoveDiacritics(this string input)`                            | Removes accents/diacritics from characters                                |
| `GenerateFileName(string extension)`                             | Generates a random file name with specified extension                     |
| `IsEmailValid(this string email)`                                | Validates if the input is a valid email address                           |
| `Truncate(this string input, int maxLength, bool keepFullWords)` | Truncates string and adds ellipsis if it exceeds length                   |
| `CountWords(this string input)`                                  | Counts the number of words in the string                                  |
| `ToBase64(this string input, Encoding encoding = null)`          | Encodes a string to Base64 using the specified encoding (UTF8 by default) |
| `FromBase64(this string input, Encoding encoding = null)`        | Decodes a Base64 string using the specified encoding (UTF8 by default)    |

## 📄 License

MIT License
