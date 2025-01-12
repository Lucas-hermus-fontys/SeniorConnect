using Domain.Commands;
using Domain.Model;
using Web.DTO.GroupChat;
using Web.Helpers;

namespace Web.DTO.DiscussionForm;

public class DiscussionFormBuilder
{
    public static FormDTO CreateFormFromParts(CollaborativeSpace space)
    {
        List<String> topics = new List<String>();
        foreach (Topic topic in space.Topics)
        {
            topics.Add(topic.Name);
        }
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
                ProfileImageUrl = space.Creator.ProfilePictureUrl,
                Email = space.Creator.Email
            },
            Comments = BuildCommentsRecursive(space.CollaborativeSpaceMessages, space.Id),
            Tags = topics
        };

        return form;
    }

    public static DiscussionFormDTO CreateFromParts(User user, List<CollaborativeSpace> collaborativeSpaces)
    {
        List<FormDTO> forms = new List<FormDTO>();

        foreach (CollaborativeSpace space in collaborativeSpaces)
        {
            List<String> topics = new List<String>();
            foreach (Topic topic in space.Topics)
            {
                topics.Add(topic.Name);
            }
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
                    ProfileImageUrl = space.Creator.ProfilePictureUrl,
                    Email = space.Creator.Email
                },
                Comments = BuildCommentsRecursive(space.CollaborativeSpaceMessages, space.Id),
                Tags = topics
            };

            forms.Add(form);
        }

        return new DiscussionFormDTO
        {
            User = new UserDTO
            {
                Id = user.Id,
                DisplayName = user.FirstName + " " + user.LastName,
                ProfileImageUrl = user.ProfilePictureUrl,
                Email = user.Email
            },
            Forms = forms
        };
    }

    private static List<CommentDTO> BuildCommentsRecursive(List<CollaborativeSpaceMessage> messages, int formId, List<CommentDTO> commentDTOs)
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
                    ProfileImageUrl = message.User.ProfilePictureUrl,
                    Email = message.User.Email
                },
                Comments = BuildCommentsRecursive(message.ChildMessages, formId),
                FormId = formId,
                ParentId = message.ParentId
            };

            commentDTOs.Add(commentDTO);
        }

        return commentDTOs;
    }

    private static List<CommentDTO> BuildCommentsRecursive(List<CollaborativeSpaceMessage> messages, int formId)
    {
        return BuildCommentsRecursive(messages, formId, new List<CommentDTO>());
    }
}
