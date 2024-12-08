using Domain.Model;

namespace Domain.Interface;

public interface IGroupChatRepository
{
    public List<CollaborativeSpace> GetGroupChatsByUserId(int userId);
}