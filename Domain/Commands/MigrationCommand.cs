using Domain.Interface;
using Server.Domain.Util;

namespace Domain.Commands
{
    public class MigrationCommand
    {
        private readonly IMigration _migration;
        private readonly ISeeder _seeder;

        public MigrationCommand(IMigration migration, ISeeder seeder)
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