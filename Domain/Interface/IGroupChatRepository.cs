using Domain.Model;

namespace Domain.Interface;

public interface IGroupChatRepository
{
    public List<CollaborativeSpace> GetGroupChatsByUserId(int userId);
    public List<CollaborativeSpaceMessage> GetGroupChatMessagesByGroupChatId(int groupChatId);
    public List<User> GetUsersByGroupChatIds(List<int> groupChatIds);

    public void CreateMessage(int userId, string message, int groupChatId);
    public CollaborativeSpace GetGroupChatById(int id);
}