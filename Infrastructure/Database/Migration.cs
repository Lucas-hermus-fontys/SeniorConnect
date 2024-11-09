using System.IO;
using Infrastructure.Util;

namespace Infrastructure.Database;

public class Migration
{
    private readonly Database _databaseUtil;

    public Migration()
    {
        _databaseUtil = ServiceLocator.GetService<Database>();
    }
    
    public void MigrateDatabase()
    {
        string query = File.ReadAllText("../Infrastructure/Database/Migrations/21-10-2024-11-14.sql");
        _databaseUtil.ExecuteQuery(query);
    }
}