namespace Domain.Model;

public class CollaborativeSpaceMessage
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public int CollaborativeSpaceId { get; set; }
    public int UserId { get; set; }
    public string Message { get; set; }
    public bool IsActive { get; set; }
    public string ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public User User { get; set; }
    public List<CollaborativeSpaceMessage> ChildMessages { get; set; } = new List<CollaborativeSpaceMessage>();

    // Add a child message
    public void AddChildMessage(CollaborativeSpaceMessage childMessage)
    {
        if (childMessage != null && !ChildMessages.Contains(childMessage))
        {
            ChildMessages.Add(childMessage);
        }
    }
}