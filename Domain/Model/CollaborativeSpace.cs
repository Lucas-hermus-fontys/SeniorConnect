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
}