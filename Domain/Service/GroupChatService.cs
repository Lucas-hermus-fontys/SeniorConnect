using Domain.Interface;
using Domain.Model;

namespace Domain.Service;

public class GroupChatService
{
    private readonly IGroupChatRepository _groupChatRepository;

    public GroupChatService(IGroupChatRepository groupChatRepository)
    {
        _groupChatRepository = groupChatRepository;
    }
    
    public List<CollaborativeSpace> GetGroupChatsByUserId(int userId)
    {
        List<CollaborativeSpace> groupChats = _groupChatRepository.GetGroupChatsByUserId(userId);
        if (groupChats.Count == 0) { return new List<CollaborativeSpace>(); }
        List<CollaborativeSpaceMessage> messages = _groupChatRepository.GetGroupChatMessagesByGroupChatId(groupChats[0].Id);
        List<User> users = _groupChatRepository.GetUsersByGroupChatIds(groupChats.Select(g => g.Id).ToList());
        messages.ForEach(message => message.User = users.FirstOrDefault(user => user.Id == message.UserId));
        users.ForEach(user => groupChats.FirstOrDefault(o => o.Id == user.CollaborativeSpaceId)?.CollaborativeSpaceUsers.Add(user));
        messages.ForEach(message => groupChats.FirstOrDefault(o => o.Id == message.CollaborativeSpaceId)?.CollaborativeSpaceMessages.Add(message));
        
        return groupChats;
    }
}