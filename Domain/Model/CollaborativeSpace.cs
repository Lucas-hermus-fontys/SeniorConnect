using Domain.Enum;

namespace Domain.Model;

public class CollaborativeSpace
{
    public int Id { get; set; }
    public string Name { get; set; }
    public CollaborativeSpaceType Type { get; set; }
    public bool IsDirectMessage { get; set; }
    public bool IsActive { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<CollaborativeSpaceMessage> CollaborativeSpaceMessages { get; set; } = new List<CollaborativeSpaceMessage>();
    public List<User> CollaborativeSpaceUsers { get; set; } = new List<User>();
    public User Creator { get; set; }
    public List<Topic> Topics { get; set; } = new List<Topic>();
    
    public void AddCollaborativeSpaceUser(User user)
    {
        if (user != null && !CollaborativeSpaceUsers.Contains(user))
        {
            CollaborativeSpaceUsers.Add(user);
        }
    }
    
    public void AddCollaborativeSpaceMessage(CollaborativeSpaceMessage message)
    {
        if (message != null && !CollaborativeSpaceMessages.Contains(message))
        {
            CollaborativeSpaceMessages.Add(message);
        }
    }
}