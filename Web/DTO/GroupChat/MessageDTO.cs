namespace Web.DTO.GroupChat;

public class MessageDTO
{
    public string Time { get; set; }
    public string Text { get; set; }
    public string Image { get; set; }
    public UserDTO User { get; set; }
}