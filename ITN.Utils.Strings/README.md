# StringExtensions for C#

A handy set of extension methods for working with strings in C#. This utility library includes commonly used string operations such as slug generation, title casing, email validation, diacritic removal, and more.

## Features

- ✅ Convert strings to URL-friendly slugs
- ✅ Capitalize the first letter of each word (title case)
- ✅ Remove diacritics from strings
- ✅ Validate email addresses
- ✅ Generate random file names
- ✅ Truncate strings with ellipsis
- ✅ Count words in a string

## Installation

You can include this extension class in your project by copying the `StringExtensions.cs` file into your solution and ensuring the required helper classes (`Slug`, `StringUtilities`) are available.

## Usage

```csharp
using ITN.Utils.Strings;

class Example
{
    void Run()
    {
        string title = "Hello world from CSharp";

        string slug = title.ToSlug(); //hello-world-from-csharp
        string titleCase = title.ToTitleCase(); // Hello World From Csharp
        string noDiacritics = "Crème brûlée".RemoveDiacritics(); // Creme brulee
        bool isEmailValid = "example@email.com".IsEmailValid(); // true
        string fileName = StringExtensions.GenerateFileName("jpg"); // e.g., 20250807105626143.jpg
        string shortText = "This is a very long sentence".Truncate(10); // "This is..."
        int wordCount = " Count   these words ".CountWords(); // 3
    }
}
```

## Method Reference

| Method                                       | Description                                                        |
| -------------------------------------------- | ------------------------------------------------------------------ |
| `ToSlug(this string input)`                  | Converts string to URL-friendly slug (lowercase, hyphen-separated) |
| `ToTitleCase(this string input)`             | Capitalizes the first letter of each word                          |
| `RemoveDiacritics(this string input)`        | Removes accents/diacritics from characters                         |
| `GenerateFileName(string extension)`         | Generates a random file name with specified extension              |
| `IsEmailValid(this string email)`            | Validates if the input is a valid email address                    |
| `Truncate(this string input, int maxLength)` | Truncates string and adds ellipsis if it exceeds length            |
| `CountWords(this string input)`              | Counts the number of words in the string                           |

## Dependencies

## License
