namespace Web.DTO.DiscussionForm;

public class CommentDTO
{
    public int Id { get; set; }
    public String Message { get; set; }
    public UserDTO User { get; set; }
    public String DisplayDate { get; set; }
    public List<CommentDTO> Comments { get; set; }
}