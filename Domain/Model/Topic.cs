namespace Domain.Model;



public class Topic
{
    
    public override bool Equals(object obj) => obj is Topic topic && Id == topic.Id && Name == topic.Name;
    public override int GetHashCode() => HashCode.Combine(Id, Name);
    
    public int Id { get; set; }
    public int CollaborativeSpaceId { get; set; }
    public String Name { get; set; }
    public List<TopicKeyword> Keywords { get; set; }
}