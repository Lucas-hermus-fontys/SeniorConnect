using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Enum;
using Domain.Interface;
using Domain.Model;
using Domain.Util;
using Web.Models;

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
        string query = @"SELECT * FROM collaborative_space WHERE type = 'FORM' AND is_active = true ORDER BY created_at desc";
        return _database.ExecuteQueryAndMap<CollaborativeSpace>(query);
    }

    public CollaborativeSpace GetById(int id)
    {
        string query =
            @"SELECT * FROM collaborative_space WHERE type = 'FORM' AND is_active = true and id = ? ORDER BY created_at desc";
        return _database.ExecuteQueryAndMap<CollaborativeSpace>(query, new List<int> { id }).FirstOrDefault();
    }

    public List<User> GetCreatorsByDiscussionFormIds(List<int> groupChatIds)
    {
        if (groupChatIds.Count == 0)
        {
            return new List<User>();
        }

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
        if (groupChatIds.Count == 0)
        {
            return new List<CollaborativeSpaceMessage>();
        }

        string query = @"
        SELECT csm.*
        FROM collaborative_space_message csm
        JOIN collaborative_space cs ON csm.collaborative_space_id = cs.id
        WHERE cs.id IN ( " + groupChatIds.GeneratePlaceholderList() + @")
        AND cs.type = 'FORM'
        AND cs.is_active = TRUE
        AND csm.is_active = TRUE ORDER BY id DESC";

        return _database.ExecuteQueryAndMap<CollaborativeSpaceMessage>(query, groupChatIds);
    }

    public List<Topic> GetTopicsByCollaborativeSpaceIds(List<int> collaborativeSpaceIds)
    {
        if (collaborativeSpaceIds.Count == 0)
        {
            return new List<Topic>();
        }

        string query = @"
                SELECT cst.collaborative_space_id, t.*
                FROM topic t
                JOIN collaborative_space_topic cst ON t.id = cst.topic_id
                WHERE cst.collaborative_space_id IN (" + collaborativeSpaceIds.GeneratePlaceholderList() + @");";

        return _database.ExecuteQueryAndMap<Topic>(query, collaborativeSpaceIds);
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

    public List<User> GetUsersByCommentIds(List<int> ids)
    {
        if (ids.Count == 0)
        {
            return new List<User>();
        }

        string query = @"
                SELECT u.*, csm.id AS collaborative_space_message_id
                FROM user u
                JOIN collaborative_space_message csm
                ON u.id = csm.user_id
                WHERE csm.id IN ( " + ids.GeneratePlaceholderList() + " );";

        return _database.ExecuteQueryAndMap<User>(query, ids);
    }

    public int CreateDiscussionForm(User user, DiscussionFormCreateRequest request)
    {
        const string query = @"
                INSERT INTO collaborative_space 
                (name, type, is_direct_message, is_active, description, created_at, updated_at) 
                VALUES (?, ?, ?, ?, ?, ?, ?);";

        _database.ExecuteQuery(query, request.Title, CollaborativeSpaceType.FORM.ToString(), false, true,
            request.Description, DateTime.Now, null);

        int newId =
            (int)_database.ExecuteQuery("SELECT * FROM collaborative_space ORDER BY ID DESC LIMIT 1;").Rows[0]["id"];

        _database.ExecuteQuery(
            "INSERT INTO collaborative_space_user (user_id, collaborative_space_id, is_creator) VALUES (?, ?, ?);",
            user.Id, newId, true
        );

        return newId;
    }

    public void UpdateDiscussionForm(DiscussionFormUpdateRequest request)
    {
        const string query = @"
        UPDATE collaborative_space 
        SET name = ?, type = ?, is_direct_message = ?, is_active = ?, description = ?, updated_at = ? 
        WHERE id = ?;";
        
        _database.ExecuteQuery(query, request.Title, CollaborativeSpaceType.FORM.ToString(), false, true,
            request.Description, DateTime.Now, request.Id);
    }

    public void CreateComment(User user, DiscussionFormEditRequest request)
    {
        const string query = @"INSERT INTO collaborative_space_message (user_id, parent_id, collaborative_space_id, message, is_active, created_at) VALUES (?, ?, ?, ?, ?, ?);";
        
        _database.ExecuteQuery(query,
            user.Id,
            request.ParentId != 0 ? request.ParentId : null,
            request.DiscussionFormId,
            request.Text,
            true,
            DateTime.Now
        );
    }
    
    public void AssignTopicsToDiscussionForm(int discussionFormId, List<int> topicIds)
    {
        const string query = @"
                INSERT INTO collaborative_space_topic 
                (collaborative_space_id, topic_id)
                VALUES (?, ?);";

        foreach (int topicId in topicIds)
        {
            _database.ExecuteQuery(query, discussionFormId, topicId);
        }
    }
}