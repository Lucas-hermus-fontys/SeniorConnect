using System.IO;
using Domain.Interface;

namespace Infrastructure.Database.Util;

public class Migration : IMigration
{
    private readonly IDatabase _database;

    public Migration(IDatabase database)
    {
        _database = database;
    }

    public void MigrateDatabase()
    {
        string query = File.ReadAllText("../Infrastructure/Database/Migrations/12-12-2024_v1.4.sql");
        _database.ExecuteQuery(query);
    }
}