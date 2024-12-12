using System;
using System.Data;
using Bogus;
using Domain.Enum;
using Domain.Interface;

namespace Infrastructure.Database;

public class Factory : IFactory
{
    private readonly IDatabase _database;

    public Factory(IDatabase database)
    {
        _database = database;
    }

    public void PopulateTestData()
    {
        for (int i = 0; i < 10; i++)
        {
            CreateTestUser(1);
            CreateTestUser(2);
            CreateTestUser(3);
        }

        CreateTestDirectMessage();
    }

    private void CreateTestUser(int roleId)
    {
        Faker faker = new Faker("nl");
        Person person = faker.Person;

        _database.ExecuteQuery(
            "INSERT INTO user (role_id, email, password, salt, active, first_name, last_name, phone_number, postal_code, country, city, date_of_birth, profile_picture_url) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?);",
            roleId,
            person.Email,
            "f62c84f98367cb3e942cfcf3d9961c990940c389de740563447a9db2cf7930dc",
            "3063cbf1fc2a09c6394560551f89464064fb072a1854d72dfc3b8552be49a313",
            1,
            person.FirstName,
            person.LastName,
            person.Phone,
            person.Address.ZipCode,
            "The netherlands",
            person.Address.City,
            person.DateOfBirth,
            // faker.Image.LoremFlickrUrl(200, 200, "people")
            faker.Image.PicsumUrl(300, 300)
        );
    }

    private void CreateTestDirectMessage()
    {
        DataTable users = _database.ExecuteQuery("SELECT * FROM user WHERE role_id = 2 and id not in (1, 2, 3) ORDER BY id LIMIT 2;");
        _database.ExecuteQuery(
            "INSERT INTO collaborative_space (name, type, is_direct_message, is_active, description, image_url, created_at, updated_at) VALUES (?, ?, ?, ?, ?, ?, ?, ?);",
            null, CollaborativeSpaceType.CHAT.ToString(), true, true, null, null, DateTime.Now, null
        );
        
        _database.ExecuteQuery(
            "INSERT INTO collaborative_space_user (user_id, collaborative_space_id, is_creator) VALUES (?, ?, ?),(?, ?, ?);",
            5, 1, false,
            8, 1, false
        );
        
        _database.ExecuteQuery(
            "INSERT INTO collaborative_space_message (user_id, collaborative_space_id, message, is_active, created_at) VALUES (?, ?, ?, ?, ?),(?, ?, ?, ?, ?),(?, ?, ?, ?, ?),(?, ?, ?, ?, ?),(?, ?, ?, ?, ?),(?, ?, ?, ?, ?),(?, ?, ?, ?, ?),(?, ?, ?, ?, ?);",
            5, 1, new Faker("nl").Lorem.Sentence(), true, DateTime.Now,
            8, 1, new Faker("nl").Lorem.Sentence(), true, DateTime.Now,
            5, 1, new Faker("nl").Lorem.Sentence(), true, DateTime.Now,
            8, 1, new Faker("nl").Lorem.Sentence(), true, DateTime.Now,
            8, 1, new Faker("nl").Lorem.Sentence(), true, DateTime.Now,
            5, 1, new Faker("nl").Lorem.Sentence(), true, DateTime.Now,
            8, 1, new Faker("nl").Lorem.Sentence(), true, DateTime.Now,
            5, 1, new Faker("nl").Lorem.Sentence(), true, DateTime.Now,
            5, 1, new Faker("nl").Lorem.Sentence(), true, DateTime.Now
        );
    }
}