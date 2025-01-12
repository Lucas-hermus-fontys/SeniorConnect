using Domain.Model;
using Web.Models;

namespace Domain.Interface;

public interface IDiscussionFormRepository
{
    public List<Topic> GetTopics();
    public List<TopicKeyword> GetKeywords();
    public List<CollaborativeSpace> GetDiscussionForms();
    public List<User> GetCreatorsByDiscussionFormIds(List<int> groupChatIds);
    public List<CollaborativeSpaceMessage> GetComments();
    public List<User> GetUsersByCommentIds(List<int> ids);
    public int CreateDiscussionForm(User user, DiscussionFormCreateRequest request);
    public CollaborativeSpace GetById(int id);
    public List<CollaborativeSpaceMessage> GetCommentsByDiscussionFormIds(List<int> discussionFormIds);
    public List<Topic> GetTopicsByCollaborativeSpaceIds(List<int> collaborativeSpaceIds);
    public void AssignTopicsToDiscussionForm(int discussionFormId, List<int> topicIds);
    public void CreateComment(User user, DiscussionFormCommentCreateRequest request);
}