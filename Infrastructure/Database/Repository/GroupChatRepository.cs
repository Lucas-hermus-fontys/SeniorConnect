using System.Collections.Generic;
using System.Data;
using Domain.Interface;
using Domain.Model;
using MySql.Data.MySqlClient;

namespace Infrastructure.Database.Repository;

public class GroupChatRepository : IGroupChatRepository
{
    private readonly IDatabase _database;

    public GroupChatRepository(IDatabase database)
    {
        _database = database;
    }

    public List<CollaborativeSpace> GetGroupChatsByUserId(int userId)
    {
        List<CollaborativeSpace> users = new List<CollaborativeSpace>();
        
        using (MySqlDatabaseCommand userCommand = new MySqlDatabaseCommand())
        {
            users = _database.ExecuteQueryWithCommand<CollaborativeSpace>(userCommand, "SELECT * FROM collaborative_space_user INNER JOIN collaborative_space ON collaborative_space_user.collaborative_space_id = collaborative_space.id WHERE collaborative_space_user.user_id = ?;", userId);
        }

        return users;
    }

}