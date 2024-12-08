namespace Domain.Util;

public static class StringExtensions
{
    public static string ToSnakeCase(this string pascalCase)
    {
        if (string.IsNullOrEmpty(pascalCase)) return pascalCase;
        return string.Concat(pascalCase.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x : x.ToString())).ToLower();
    }

    public static string ToPascalCase(this string snakeCase)
    {
        return string.Join(string.Empty, snakeCase.Split('_').Select(word => char.ToUpper(word[0]) + word.Substring(1).ToLower()));
    }
}