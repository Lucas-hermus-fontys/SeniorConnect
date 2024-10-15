using SeniorConnect.Util;

namespace SeniorConnect.DataAccess;

public class Migration
{
    private readonly Database _databaseUtil;

    public Migration()
    {
        _databaseUtil = ServiceLocator.GetService<Database>();
    }
    
    public void MigrateDatabase()
    {
        string query = File.ReadAllText("DataAccess/Migrations/14-10-2024-15.51.sql");
        Console.WriteLine("Running migrations!");
        _databaseUtil.ExecuteQuery(query);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Done!");
        Console.ResetColor();
    }
}