using Domain.Model;

namespace Web.DTO.GroupChat
{
    public class GroupChatBuilder
    {
        public static GroupChatDTO CreateFromParts(User user)
        {
            return new GroupChatDTO
            {
                User = new UserDTO
                {
                    Id = user.Id,
                    DisplayName = user.FirstName + " " + user.LastName,
                    ProfileImageUrl = user.ProfilePictureUrl
                },
                Chats = new List<ChatDTO>
                {
                    new ChatDTO
                    {
                        Name = "Steve Harvey",
                        Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQfdW6ZBMcdc9kFg2oIJeAqffG6msNMZ5e2zQ&s",
                        LastMessage = 
                            new MessageDTO
                            {
                                Time = "9:12",
                                Text = "Hello Lucas Hermus!",
                                Image = null,
                                User = new UserDTO
                                {
                                    Id = 7,
                                    DisplayName = "Steve Harvey",
                                    ProfileImageUrl =
                                        "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQfdW6ZBMcdc9kFg2oIJeAqffG6msNMZ5e2zQ&s"
                                }
                            },
                    },
                    new ChatDTO
                    {
                        Name = "John krasinski ",
                        Image = "https://hips.hearstapps.com/hmg-prod/images/john-krasinski-GettyImages-517027236_1600.jpg?resize=980:*",
                        LastMessage = new MessageDTO
                        {
                            Time = "14:34",
                            Text = "Hi how are you today?",
                            Image = null,
                            User = new UserDTO
                            {
                                Id = 8,
                                DisplayName = "john krasinski",
                                ProfileImageUrl =
                                    "https://hips.hearstapps.com/hmg-prod/images/john-krasinski-GettyImages-517027236_1600.jpg?resize=980:*"
                            }
                        }
                    }
                },
                Messages = new List<MessageDTO>
                {
                    new MessageDTO
                    {
                        Time = "9:12",
                        Text = "Hello Lucas Hermus!",
                        Image = null,
                        User = new UserDTO
                        {
                            Id = 7,
                            DisplayName = "Steve Harvey",
                            ProfileImageUrl =
                                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQfdW6ZBMcdc9kFg2oIJeAqffG6msNMZ5e2zQ&s"
                        }
                    },
                    new MessageDTO
                    {
                        Time = "9:12",
                        Text = "Hello Steve!",
                        Image = null,
                        User = new UserDTO
                        {
                            Id = 5,
                            DisplayName = "Lucas hermus",
                        }
                    }
                }
            };
        }
    }
}