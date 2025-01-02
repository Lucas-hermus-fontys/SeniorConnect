using Domain.Commands;
using Domain.Model;
using Web.DTO.GroupChat;
using Web.Helpers;

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
                Id = space.Id,
                DisplayDate = DateFormatter.FormatDifference(space.CreatedAt),
                Description = space.Description,
                Title = space.Name,
                User = new UserDTO
                {
                    Id = space.Creator.Id,
                    DisplayName = space.Creator.FirstName + " " + space.Creator.LastName,
                    ProfileImageUrl = space.Creator.ProfilePictureUrl
                },
                Comments = BuildCommentsRecursive(space.CollaborativeSpaceMessages)
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

    private static List<CommentDTO> BuildCommentsRecursive(List<CollaborativeSpaceMessage> messages, List<CommentDTO> commentDTOs)
    {
        if (!messages.Any())
        {
            return commentDTOs;
        }

        foreach (CollaborativeSpaceMessage message in messages)
        {
            CommentDTO commentDTO = new CommentDTO
            {
                Id = message.Id,
                DisplayDate = DateFormatter.FormatDifference(message.CreatedAt),
                Message = message.Message,
                User = new UserDTO
                {
                    Id = message.User.Id,
                    DisplayName = message.User.FirstName + " " + message.User.LastName,
                    ProfileImageUrl = message.User.ProfilePictureUrl
                },
                Comments = BuildCommentsRecursive(message.ChildMessages)
            };
            
            commentDTOs.Add(commentDTO);
        }
        
        return commentDTOs;
    }

    private static List<CommentDTO> BuildCommentsRecursive(List<CollaborativeSpaceMessage> messages)
    {
        return BuildCommentsRecursive(messages, new List<CommentDTO>());
    }
}