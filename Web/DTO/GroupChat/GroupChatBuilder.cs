using Domain.Model;

namespace Web.DTO.GroupChat
{
    public class GroupChatBuilder
    {
        public static GroupChatDTO CreateFromParts(User user, List<CollaborativeSpace> collaborativeSpaces)
        {
            List<ChatDTO> chats = new List<ChatDTO>();
            List<MessageDTO> messages = new List<MessageDTO>();

            UserDTO userDTO = new UserDTO
            {
                Id = user.Id,
                DisplayName = user.FirstName + " " + user.LastName,
                ProfileImageUrl = user.ProfilePictureUrl
            };

            if (collaborativeSpaces.Count == 0)
            {
                return new GroupChatDTO
                {
                    User = userDTO,
                    Chats = new List<ChatDTO>(),
                    Messages = new List<MessageDTO>()
                };
            }

            foreach (var spaceMessage in collaborativeSpaces[0].CollaborativeSpaceMessages)
            {
                MessageDTO message = new MessageDTO();
                message.Text = spaceMessage.Message;
                message.User = new UserDTO
                {
                    Id = spaceMessage.User.Id,
                    DisplayName = spaceMessage.User.FirstName + " " + spaceMessage.User.LastName,
                    ProfileImageUrl = spaceMessage.User.ProfilePictureUrl
                };
                message.Time = spaceMessage.CreatedAt.ToString("HH:mm");
                messages.Add(message);
            }

            foreach (var space in collaborativeSpaces)
            {
                ChatDTO chat = new ChatDTO();
                if (space.IsDirectMessage)
                {
                    User recievingUser = space.CollaborativeSpaceUsers.FirstOrDefault(obj => obj.Id != user.Id);
                    if (recievingUser == null) { throw new ArgumentNullException(nameof(recievingUser), "User cannot be null"); }

                    chat.Image = recievingUser.ProfilePictureUrl;
                    chat.Name = recievingUser.FirstName + " " + recievingUser.LastName;
                }
                else
                {
                    chat.Image = space.ImageUrl;
                    chat.Name = space.Name;
                }
                
                chats.Add(chat);
            }
            
            return new GroupChatDTO
            {
                Id = collaborativeSpaces[0].Id,
                User = userDTO,
                Chats = chats,
                Messages = messages,
            };
        }
        public static GroupChatDTO CreateMessagesFromParts(User user, CollaborativeSpace groupChat)
        {
            List<ChatDTO> chats = new List<ChatDTO>();
            List<MessageDTO> messages = new List<MessageDTO>();

            UserDTO userDTO = new UserDTO
            {
                Id = user.Id,
                DisplayName = user.FirstName + " " + user.LastName,
                ProfileImageUrl = user.ProfilePictureUrl
            };
            
            foreach (var spaceMessage in groupChat.CollaborativeSpaceMessages)
            {
                MessageDTO message = new MessageDTO();
                message.Text = spaceMessage.Message;
                message.User = new UserDTO
                {
                    Id = spaceMessage.User.Id,
                    DisplayName = spaceMessage.User.FirstName + " " + spaceMessage.User.LastName,
                    ProfileImageUrl = spaceMessage.User.ProfilePictureUrl
                };
                message.Time = spaceMessage.CreatedAt.ToString("HH:mm");
                messages.Add(message);
            }
            
            return new GroupChatDTO
            {
                Id = groupChat.Id,
                User = userDTO,
                Chats = chats,
                Messages = messages,
            };
        }
    }
}