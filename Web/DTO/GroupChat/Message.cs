namespace Web.DTO.GroupChat;

public class Message
{
    public string Time { get; set; }
    public string Text { get; set; }
    public string Image { get; set; }
    public User User { get; set; }
}