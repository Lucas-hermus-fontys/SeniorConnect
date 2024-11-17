using System;
using System.Data;
using Infrastructure.Database.Models;
using Infrastructure.Util;

namespace Infrastructure.Database.Repository;

public class UserRepository
{
    private readonly Database _databaseUtil;

    public UserRepository()
    {
        _databaseUtil = ServiceLocator.GetService<Database>();
    }

    public void CreateNewUser(int roleId, string email, string password, string salt) // ToDo; User user
    {
        _databaseUtil.ExecuteQuery(
            "INSERT INTO user (role_id, email, password, salt, active) VALUES (?, ?, ?, ?, ?);",
            roleId,
            email,
            password,
            salt,
            1
        );
    }

    public DataRow GetByEmail(string email)
    {
        DataTable result = _databaseUtil.ExecuteQuery("Select * from user where email = ?;", email);
        if (result.Rows.Count == 0)
        {
            return null;
        }

        return result.Rows[0];
    }

    public void PrintAllUsers()
    {
        string query = "SELECT * FROM Users"; // SQL query to fetch all users
        DataTable usersDataTable = _databaseUtil.ExecuteQuery(query); // Call your ExecuteQuery function

        // Loop through the rows of the DataTable
        foreach (DataRow row in usersDataTable.Rows)
        {
            // Accessing individual columns by name or index
            string userName = row["email"].ToString(); // Replace "UserName" with your actual column name

            // Print out the user data
            Console.WriteLine($"User: {userName}");
        }
    }
}