using SeniorConnect.Util;

namespace SeniorConnect.DataAccess
{
    public class Seeder
    {
        private readonly Database _databaseUtil;

        public Seeder()
        {
            _databaseUtil = ServiceLocator.GetService<Database>();
        }

        public void SeedDatabase()
        {
        }
    }
}
