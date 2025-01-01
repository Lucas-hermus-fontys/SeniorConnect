namespace Domain.Model;

public class Topic
{
    public int Id { get; set; }
    public String Name { get; set; }
    public List<TopicKeyword> Keywords { get; set; }
}