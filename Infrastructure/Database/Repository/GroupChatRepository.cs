using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Domain.Interface;
using Domain.Model;
using Domain.Util;
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
        string query = @"SELECT * FROM collaborative_space_user 
                        INNER JOIN collaborative_space 
                        ON collaborative_space_user.collaborative_space_id = collaborative_space.id
                        WHERE collaborative_space_user.user_id = ? AND type = 'CHAT'";

        return _database.ExecuteQueryAndMap<CollaborativeSpace>(query, new List<int> { userId });
    }

    public CollaborativeSpace GetGroupChatById(int id)
    {
        string query = "SELECT * FROM collaborative_space WHERE `id` = ?";
        return _database.ExecuteQueryAndMap<CollaborativeSpace>(query, new List<int> { id }).First();
    }

    public List<CollaborativeSpaceMessage> GetGroupChatMessagesByGroupChatId(int groupChatId)
    {
        string query = "SELECT * FROM collaborative_space_message WHERE collaborative_space_id = ?";
        return _database.ExecuteQueryAndMap<CollaborativeSpaceMessage>(query, new List<int> { groupChatId });
    }

    public List<User> GetUsersByGroupChatIds(List<int> groupChatIds)
    {
        string query = @"
                    SELECT u.*, csi.collaborative_space_id, c.* FROM user u
                    JOIN collaborative_space_user csi ON u.id = csi.user_id
                    JOIN collaborative_space c ON csi.collaborative_space_id = c.id
                    WHERE c.id IN ( " + groupChatIds.GeneratePlaceholderList() + " );";
        
        return _database.ExecuteQueryAndMap<User>(query, groupChatIds);
    }

    public void CreateMessage(int userId, string message, int groupChatId)
    {
        _database.ExecuteQuery(
            "INSERT INTO collaborative_space_message (user_id, collaborative_space_id, message, is_active, created_at) VALUES (?, ?, ?, ?, ?)",
            userId,
            groupChatId,
            message,
            true,
            DateTime.Now
        );
    }
}