namespace Domain.Util;

using System;

public static class EnumExtensions
{
    public static T ParseEnum<T>(string value) where T : Enum
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }
    
    public static object ParseEnum(Type enumType, string value)
    {
        return Enum.Parse(enumType, value, true);
    }
}