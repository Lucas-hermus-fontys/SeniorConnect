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
        _databaseUtil.ExecuteQuery(query);
    }
}