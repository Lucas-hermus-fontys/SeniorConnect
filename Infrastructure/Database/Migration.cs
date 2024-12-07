using System.IO;
using Domain.Interface;

namespace Infrastructure.Database;

public class Migration : IMigration
{
    private readonly IDatabase _database;

    public Migration(IDatabase database)
    {
        _database = database;
    }
    
    public void MigrateDatabase()
    {
        string query = File.ReadAllText("../Infrastructure/Database/Migrations/21-10-2024-11-14.sql");
        _database.ExecuteQuery(query);
    }
}