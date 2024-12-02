using Infrastructure.Database;
using Server.Domain.Util;

namespace Domain.Commands
{
    public class MigrationCommand
    {
        private readonly Migration _migration;
        private readonly Seeder _seeder;

        public MigrationCommand(Migration migration, Seeder seeder)
        {
            _migration = migration;
            _seeder = seeder;
        }

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