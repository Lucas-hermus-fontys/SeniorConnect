using System.Data;

namespace Domain.Util;

public static class DataRecordExtensions
{
    public static bool HasColumn(this IDataRecord record, string columnName)
    {
        for (int i = 0; i < record.FieldCount; i++)
        {
            if (record.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }
        return false;
    }
}