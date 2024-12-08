namespace Domain.Model;

public class CollaborativeSpaceMessage
{
    public int Id { get; set; }
    public int CollaborativeSpaceId { get; set; }
    public int UserId { get; set; }
    
    public User User { get; set; }
}