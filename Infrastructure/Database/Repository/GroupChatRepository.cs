using System.Collections.Generic;
using System.Data;
using System.Linq;
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
        List<CollaborativeSpace> groupChats = new List<CollaborativeSpace>();
        
        using (MySqlDatabaseCommand userCommand = new MySqlDatabaseCommand())
        {
            groupChats = _database.ExecuteQueryWithCommand<CollaborativeSpace>(userCommand, "SELECT * FROM collaborative_space_user INNER JOIN collaborative_space ON collaborative_space_user.collaborative_space_id = collaborative_space.id WHERE collaborative_space_user.user_id = ?;", userId);
            
            List<CollaborativeSpaceMessage> messages = _database.ExecuteQueryWithCommand<CollaborativeSpaceMessage>(userCommand, "SELECT id, collaborative_space_id, user_id FROM collaborative_space_message WHERE collaborative_space_id in (" + string.Join(",", groupChats.Select(g => g.Id).ToList()) + ");");
            
            List<User> users = _database.ExecuteQueryWithCommand<User>(userCommand, "SELECT * FROM user WHERE id in (" + string.Join(",", messages.Select(g => g.Id).ToList()) + ");");
            
            messages.ForEach(message => message.User = users.FirstOrDefault(user => user.Id == message.UserId));
            
            messages.ForEach(message => groupChats.FirstOrDefault(o => o.Id == message.CollaborativeSpaceId)?.CollaborativeSpaceMessages.Add(message));

        }

        return groupChats;
    }

}