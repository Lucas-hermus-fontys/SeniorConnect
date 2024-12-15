namespace Web.DTO.GroupChat;

public class GroupChatDTO
{
    public int Id { get; set; }
    public UserDTO User { get; set; }
    public List<ChatDTO> Chats { get; set; }
    
    public List<MessageDTO> Messages { get; set; }
}