using Domain.Model;
using Web.Models;

namespace Domain.Interface;

public interface IDiscussionFormRepository
{
    public List<Topic> GetTopics();
    public List<TopicKeyword> GetKeywords();
    public List<CollaborativeSpace> GetDiscussionForms();
    public List<User> GetCreatorsByGroupChatIds(List<int> groupChatIds);
    public List<CollaborativeSpaceMessage> GetComments();
    public List<User> GetUserByCommentIds(List<int> ids);
    public void CreateDiscussionForm(User user, DiscussionFormCreateRequest request);
}