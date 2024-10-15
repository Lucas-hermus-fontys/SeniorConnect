using SeniorConnect.DataAccess;

namespace SeniorConnect.Commands
{
    public class MigrationCommand
    {
        private readonly Migration migration = new Migration();

        public void MigrateDatabase()
        {
            migration.MigrateDatabase();
        }
    }
}