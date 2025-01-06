namespace Domain.Util;

public static class ListExtentions
{
    public static string GeneratePlaceholderList(this List<int> values)
    {
        if (values == null || !values.Any())
        {
            return string.Empty;
        }

        return string.Join(", ", Enumerable.Repeat("?", values.Count));
    }
}