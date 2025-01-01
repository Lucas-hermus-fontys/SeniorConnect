using System.Collections.Generic;
using System.Linq;
using Domain.Interface;
using Domain.Model;
using Domain.Util;

namespace Infrastructure.Database.Repository;

public class DiscussionFormRepository : IDiscussionFormRepository
{
    private readonly IDatabase _database;

    public DiscussionFormRepository(IDatabase database)
    {
        _database = database;
    }

    public List<Topic> GetTopics()
    {
        string query = "SELECT * FROM topic";
        return _database.ExecuteQueryAndMap<Topic>(query);
    }

    public List<TopicKeyword> GetKeywords()
    {
        string query = "SELECT * FROM topic_keyword";
        return _database.ExecuteQueryAndMap<TopicKeyword>(query);
    }

    public List<CollaborativeSpace> GetDiscussionForms()
    {
        string query = @"SELECT * FROM collaborative_space WHERE type = 'FORM' AND is_active = true";
        return _database.ExecuteQueryAndMap<CollaborativeSpace>(query);
    }

    public List<User> GetCreatorsByGroupChatIds(List<int> groupChatIds)
    {
        string query = $@"
            SELECT DISTINCT u.*, csi.collaborative_space_id
            FROM user u
            JOIN collaborative_space_user csi ON u.id = csi.user_id
            JOIN collaborative_space c ON csi.collaborative_space_id = c.id
            WHERE c.id IN ({groupChatIds.GeneratePlaceholderList()}) AND csi.is_creator = true;";


        return _database.ExecuteQueryAndMap<User>(query, groupChatIds);
    }

    public List<CollaborativeSpaceMessage> GetCommentsByDiscussionFormIds(List<int> groupChatIds)
    {
        string query = @"
                    SELECT u.*, csi.collaborative_space_id, c.* FROM user u
                    JOIN collaborative_space_message csi ON u.id = csi.user_id
                    JOIN collaborative_space c ON csi.collaborative_space_id = c.id
                    WHERE c.id IN ( " + groupChatIds.GeneratePlaceholderList() + " );";
        return _database.ExecuteQueryAndMap<CollaborativeSpaceMessage>(query, groupChatIds);
    }

    public List<CollaborativeSpaceMessage> GetComments()
    {
        string query = @"
            SELECT csc.*
            FROM collaborative_space_message csc
            JOIN collaborative_space cs ON csc.collaborative_space_id = cs.id
            WHERE cs.type = 'FORM'
            AND cs.is_active = TRUE
            AND csc.is_active = TRUE";

        return _database.ExecuteQueryAndMap<CollaborativeSpaceMessage>(query);
    }

    public List<User> GetUserByCommentIds(List<int> ids)
    {
        string query = @"
                SELECT u.*, csm.id AS collaborative_space_message_id
                FROM user u
                JOIN collaborative_space_message csm
                ON u.id = csm.user_id
                WHERE csm.id IN ( " + ids.GeneratePlaceholderList() + " );";
        
        return _database.ExecuteQueryAndMap<User>(query, ids);
    }
    
    
}