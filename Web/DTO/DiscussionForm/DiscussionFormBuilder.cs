using Domain.Model;

namespace Web.DTO.DiscussionForm;

public class DiscussionFormBuilder
{
    public static DiscussionFormDTO CreateFromParts(User user, List<CollaborativeSpace> collaborativeSpaces)
    {
        
        List<FormDTO> forms = new List<FormDTO>();


        foreach (CollaborativeSpace space in collaborativeSpaces)
        {
            FormDTO form = new FormDTO
            {
                DateTime = space.CreatedAt.ToString("dd-MM-yyyy HH:mm"),
                Description = space.Description,
                Title = space.Name,
                User = new UserDTO
                {
                    Id = space.Creator.Id,
                    DisplayName = space.Creator.FirstName + " " + space.Creator.LastName,
                    ProfileImageUrl = space.Creator.ProfilePictureUrl
                }
            };
            
            forms.Add(form);
        }
        
        return new DiscussionFormDTO
        {
            
            User = new UserDTO
            {
                Id = user.Id,
                DisplayName = user.FirstName + " " + user.LastName,
                ProfileImageUrl = user.ProfilePictureUrl
            },
            
            Forms = forms
        };
    }
}