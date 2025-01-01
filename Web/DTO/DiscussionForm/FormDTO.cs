namespace Web.DTO.DiscussionForm;

public class FormDTO
{
    public UserDTO User { get; set; }
    public String DateTime { get; set; }
    public String Title { get; set; }
    public String Description { get; set; }
    
    public List<CommentDTO> Comments { get; set; }
}