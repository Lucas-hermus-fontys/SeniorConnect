namespace Domain.Util;

public class EnumExtensions
{
    public static object ParseEnum(Type enumType, string value)
    {
        return System.Enum.Parse(enumType, value, true);
    }
}