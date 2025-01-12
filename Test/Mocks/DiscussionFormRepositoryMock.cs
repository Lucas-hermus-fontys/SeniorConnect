using Domain.Interface;
using Domain.Model;
using Test.Util;
using Web.Models;

namespace Test.Mocks;

public class DiscussionFormRepositoryMock : IDiscussionFormRepository
{
    public bool AssignTopicsCalled { get; private set; }

    public DiscussionFormRepositoryMock()
    {
        AssignTopicsCalled = false;
    }

    public List<CollaborativeSpaceMessage> GetComments()
    {
        return null;
    }
    
    public int CreateDiscussionForm(User user, DiscussionFormCreateRequest request)
    {
        return 1;  
    }

    public CollaborativeSpace GetById(int id)
    {
        return MockMaker.GetBaseDiscussionForm();
    }

    public List<CollaborativeSpace> GetDiscussionForms()
    {
        return null;
    }

    public void AssignTopicsToDiscussionForm(int discussionFormId, List<int> topicIds)
    {
        AssignTopicsCalled = true;
    }

    public List<Topic> GetTopics()
    {
        return  MockMaker.GetTopics();
    }

    public List<TopicKeyword> GetKeywords()
    {
        return MockMaker.GetKeywords();
    }

    public List<User> GetCreatorsByDiscussionFormIds(List<int> discussionFormIds)
    {
        return new List<User> { };
    }

    public List<Topic> GetTopicsByCollaborativeSpaceIds(List<int> discussionFormIds)
    {
        return new List<Topic> { };
    }

    public List<CollaborativeSpaceMessage> GetCommentsByDiscussionFormIds(List<int> discussionFormIds)
    {
        return new List<CollaborativeSpaceMessage> { };
    }

    public List<User> GetUsersByCommentIds(List<int> commentIds)
    {
        return new List<User> { };
    }

    public void AddTopic(Topic topic)
    {
    }

    public void AddKeyword(TopicKeyword keyword)
    {
    }

    public void UpdateDiscussionForm(DiscussionFormUpdateRequest request)
    {
    }

    public void CreateComment(User user, DiscussionFormEditRequest request)
    {
        
    }
}
