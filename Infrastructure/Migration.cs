using SeniorConnect.Domain.Util;

namespace SeniorConnect.Infrastructure;

public class Migration
{
    private readonly Database _databaseUtil;

    public Migration()
    {
        _databaseUtil = ServiceLocator.GetService<Database>();
    }
    
    public void MigrateDatabase()
    {
        string query = File.ReadAllText("Infrastructure/Migrations/21-10-2024-11-14.sql");
        Console.WriteLine("Running migrations!");
        _databaseUtil.ExecuteQuery(query);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Done!");
        Console.ResetColor();
    }
}