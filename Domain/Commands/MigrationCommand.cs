using SeniorConnect.Infrastructure;

namespace SeniorConnect.Domain.Commands
{
    public class MigrationCommand
    {
        private readonly Migration _migration = new Migration();

        public void MigrateDatabase()
        {
            _migration.MigrateDatabase();
        }
    }
}