using SeniorConnect.Domain.Util;
using SeniorConnect.Infrastructure;

namespace SeniorConnect.Domain.Commands
{
    public class MigrationCommand
    {
        private readonly Migration _migration = new();
        private readonly Seeder _seeder = new();

        public void MigrateDatabase(String[] args)
        {
            LoggingUtil.Log("Migrating database...");
            _migration.MigrateDatabase();
            
            if (args.Length > 1 && args[1] == "seed")
            {
                LoggingUtil.Log("Seeding database");
                _seeder.SeedDatabase();
            }
            LoggingUtil.Log("Done!", ConsoleColor.Green);
        }
    }
}