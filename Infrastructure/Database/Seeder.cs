using Infrastructure.Util;

namespace Infrastructure.Database
{
    public class Seeder
    {
        private readonly Database _database;

        public Seeder()
        {
            _database = ServiceLocator.GetService<Database>();
        }

        public void SeedDatabase()
        {
            //user_role
            _database.ExecuteQuery("INSERT INTO user_role (name) VALUES (?);", "admin");
            _database.ExecuteQuery("INSERT INTO user_role (name) VALUES (?);", "member");
            _database.ExecuteQuery("INSERT INTO user_role (name) VALUES (?);", "contributor");

            //user
            _database.ExecuteQuery("INSERT INTO user (role_id, email, password, salt, active, first_name, last_name, phone_number) VALUES (?, ?, ?, ?, ?, ?, ?, ?);", 1, "member@member.com", "e31ab643c44f7a0ec824b59d1194d60dac334200d845e61d2d289daa0f087ea4", "f7225388c1d69d57e6251c9fda50cbbf9e05131e5adb81e5aa0422402f048162", 1, "member_firstname", "member_lastname", "06-12345678");
            _database.ExecuteQuery("INSERT INTO user (role_id, email, password, salt, active, first_name, last_name, phone_number) VALUES (?, ?, ?, ?, ?, ?, ?, ?);", 1, "admin@admin.com", "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918", "f7225388c1d69d57e6251c9fda50cbbf9e05131e5adb81e5aa0422402f048162", 1, "admin_firstname", "admin_firstname", "06-12345678");
            _database.ExecuteQuery("INSERT INTO user (role_id, email, password, salt, active, first_name, last_name, phone_number) VALUES (?, ?, ?, ?, ?, ?, ?, ?);", 1, "contributor@contributor.com", "7ee8a8789d5be8d2be3b35505ab433d8e7ab18a25ad4abf066a47b0bd86ce851", "f7225388c1d69d57e6251c9fda50cbbf9e05131e5adb81e5aa0422402f048162", 1, "contributor_firstname", "contributor_firstname", "06-12345678");
        }
    }
}