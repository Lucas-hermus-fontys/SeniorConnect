using System.Drawing;

namespace Server.Domain.Util;

public class LoggingUtil
{
    public static void Log(string message, ConsoleColor color = ConsoleColor.Gray)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ForegroundColor = ConsoleColor.Gray;
    }
}