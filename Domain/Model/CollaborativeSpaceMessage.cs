namespace Domain.Model;

public class CollaborativeSpaceMessage
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public int CollaborativeSpaceId { get; set; }
    public int UserId { get; set; }
    public String Message { get; set; }
    public bool IsActive { get; set; }
    public String ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public User User { get; set; }
    public List<CollaborativeSpaceMessage> ChildMessages { get; set; } = new List<CollaborativeSpaceMessage>();
}