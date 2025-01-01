namespace Domain.Model;

public class TopicKeyword
{
    public int Id { get; set; }
    public int TopicId { get; set; }
    public Topic Topic { get; set; }
    public String Name { get; set; }
    public double Weight { get; set; }
}