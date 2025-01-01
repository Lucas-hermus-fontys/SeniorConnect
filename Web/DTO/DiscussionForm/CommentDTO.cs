namespace Web.DTO.DiscussionForm;

public class CommentDTO
{
    String Message { get; set; }
    String Datetime { get; set; }
    UserDTO User { get; set; }
}