namespace Web.Helpers;

public static class DateFormatter
{
    public static String FormatDifference(DateTime date)
    {
        TimeSpan difference = DateTime.Now - date;
        int daysDifference = difference.Days;

        if (daysDifference == 0)
        {
            return date.ToString("HH:mm");
        }

        if (daysDifference <= 7)
        {
            return $"{daysDifference}d";
        }

        return date.ToString("dd-MM-yyyy HH:mm");
    }
}