namespace Web.DTO.DiscussionForm;

public class FormDTO
{
    public UserDTO User { get; set; }
    public int Id { get; set; }
    public String DisplayDate { get; set; }
    public String Title { get; set; }
    public String Description { get; set; }
    public List<CommentDTO> Comments { get; set; }
    public List<String> Tags { get; set; }
}