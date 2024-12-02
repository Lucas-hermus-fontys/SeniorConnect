namespace Web.DTO.GroupChat
{
    public class GroupChatBuilder
    {
        public static GroupChatDto CreateFromParts()
        {
            return new GroupChatDto
            {
                User = new User
                {
                    Id = 5,
                    DisplayName = "Lucas Hermus",
                    ProfileImageUrl = null,
                },
                Chats = new List<Chat>
                {
                    new Chat
                    {
                        Name = "Steve Harvey",
                        Messages = new List<Message>
                        {
                            new Message
                            {
                                Time = "9:12",
                                Text = "Hello Lucas Hermus!",
                                Image = null,
                                User = new User
                                {
                                    Id = 7,
                                    DisplayName = "Steve Harvey",
                                    ProfileImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQfdW6ZBMcdc9kFg2oIJeAqffG6msNMZ5e2zQ&s"
                                }
                            },
                            new Message
                            {
                                Time = "9:14",
                                Text = "Hello Steve!",
                                Image = null,
                                User = new User
                                {
                                    Id = 5,
                                    DisplayName = "Steve Harvey",
                                    ProfileImageUrl = null
                                }
                            },
                        }
                    },
                    new Chat
                    {
                        Name = "John krasinski ",
                        Messages = new List<Message>
                        {
                            new Message
                            {
                                Time = "14:34",
                                Text = "Hi how are you today?",
                                Image = null,
                                User = new User
                                {
                                    Id = 8,
                                    DisplayName = "john krasinski",
                                    ProfileImageUrl = "https://hips.hearstapps.com/hmg-prod/images/john-krasinski-GettyImages-517027236_1600.jpg?resize=980:*"
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}