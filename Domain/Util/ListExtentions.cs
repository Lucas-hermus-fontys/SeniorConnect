namespace Domain.Util;

public static class ListExtentions
{
    public static string GeneratePlaceholderList(this List<int> values)
    {
        if (values == null || values.Count == 0)
        {
            return string.Empty;
        }

        return string.Join(", ", Enumerable.Repeat("?", values.Count));
    }
}