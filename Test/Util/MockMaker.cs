using Domain.Enum;
using Domain.Model;

namespace Test.Util
{
    public static class MockMaker
    {
        public static CollaborativeSpace GetBaseDiscussionForm()
        {
            var messages = new List<CollaborativeSpaceMessage>
            {
                GetMessage(),
                GetMessage()
            };
            messages.FirstOrDefault().ChildMessages.Add(GetMessage());
            return new CollaborativeSpace
            {
                Id = 1,
                Name = "Welke apps zijn nuttig voor boodschappen doen?",
                Type = CollaborativeSpaceType.FORM,
                IsDirectMessage = false,
                IsActive = true,
                Description = "Zijn er apps die het makkelijker maken om boodschappen te doen zonder de deur uit te hoeven? Ik zou graag willen weten welke apps handig zijn om alles thuisbezorgd te krijgen.",
                ImageUrl = "https://example.com/image.png",
                CreatedAt = DateTime.UtcNow.AddMonths(-1),
                UpdatedAt = DateTime.UtcNow,
                CollaborativeSpaceMessages = messages,
                CollaborativeSpaceUsers = new List<User>
                {
                    GetUser(),
                    GetUser()
                },
                Creator = GetUser(),
                Topics = new List<Topic>
                {
                    new Topic { Id = 1, Name = "Team Goals" },
                    new Topic { Id = 2, Name = "Project Planning" }
                }
            };
        }

        public static CollaborativeSpaceMessage GetMessage()
        {
            return new CollaborativeSpaceMessage
            {
                Id = 1,
                ParentId = 0,
                CollaborativeSpaceId = 1,
                UserId = GetUser().Id,
                Message = "This is a message",
                IsActive = true,
                ImageUrl = GetUser().ProfilePictureUrl,
                CreatedAt = DateTime.UtcNow.AddDays(-7),
                UpdatedAt = DateTime.UtcNow,
                User = GetUser(),
            };
        }

        public static User GetUser()
        {
            return new User
            {
                Id = 1,
                UserRole = new UserRole (1,"Admin" ),
                Email = "johndoe@example.com",
                Password = "password123",
                Salt = "salt",
                Active = true,
                GoogleId = "google-id-1",
                FacebookId = "facebook-id-1",
                FirstName = "John",
                LastName = "Doe",
                NameAffix = "Mr.",
                Address = "123 Main St",
                PostalCode = "12345",
                Country = "USA",
                City = "New York",
                PhoneNumber = "123-456-7890",
                DateOfBirth = DateTime.UtcNow.AddYears(-30),
                ProfilePictureUrl = "https://example.com/johndoe.png",
                CreatedAt = DateTime.UtcNow.AddMonths(-6),
                UpdatedAt = DateTime.UtcNow
            };
        }

        public static List<Topic> GetTopics()
        {
            var topics = new List<Topic>
            {
                new Topic
                {
                    Id = 1,
                    CollaborativeSpaceId = 1,
                    Name = "Gezondheid en Welzijn",
                    Keywords = new List<TopicKeyword>
                    {
                        new TopicKeyword { Id = 1, TopicId = 1, Name = "Gezondheid", Weight = 0.9 },
                        new TopicKeyword { Id = 2, TopicId = 1, Name = "Welzijn", Weight = 0.8 },
                        new TopicKeyword { Id = 3, TopicId = 1, Name = "Chronische ziektes", Weight = 0.8 },
                        new TopicKeyword { Id = 4, TopicId = 1, Name = "Medicatie", Weight = 0.7 },
                        new TopicKeyword { Id = 5, TopicId = 1, Name = "Voeding", Weight = 0.7 },
                        new TopicKeyword { Id = 6, TopicId = 1, Name = "Oefenroutines", Weight = 0.6 },
                        new TopicKeyword { Id = 7, TopicId = 1, Name = "Mentale gezondheid", Weight = 0.8 },
                        new TopicKeyword { Id = 8, TopicId = 1, Name = "Gezondheidszorg", Weight = 0.6 },
                        new TopicKeyword { Id = 9, TopicId = 1, Name = "Therapie", Weight = 0.6 },
                        new TopicKeyword { Id = 10, TopicId = 1, Name = "Lichaamsbeweging", Weight = 0.6 },
                        new TopicKeyword { Id = 11, TopicId = 1, Name = "Preventie", Weight = 0.6 },
                        new TopicKeyword { Id = 12, TopicId = 1, Name = "Levensstijl", Weight = 0.5 }
                    }
                },
                new Topic
                {
                    Id = 2,
                    CollaborativeSpaceId = 2,
                    Name = "Pensioenplanning",
                    Keywords = new List<TopicKeyword>
                    {
                        new TopicKeyword { Id = 13, TopicId = 2, Name = "Pensioen", Weight = 0.9 },
                        new TopicKeyword { Id = 14, TopicId = 2, Name = "Financiële planning", Weight = 0.8 },
                        new TopicKeyword { Id = 15, TopicId = 2, Name = "Investeringen", Weight = 0.7 },
                        new TopicKeyword { Id = 16, TopicId = 2, Name = "Pensioenfondsen", Weight = 0.8 },
                        new TopicKeyword { Id = 17, TopicId = 2, Name = "Financiële zekerheid", Weight = 0.8 },
                        new TopicKeyword { Id = 18, TopicId = 2, Name = "Spaargeld", Weight = 0.7 },
                        new TopicKeyword { Id = 19, TopicId = 2, Name = "Pensioenleeftijd", Weight = 0.6 },
                        new TopicKeyword { Id = 20, TopicId = 2, Name = "Sociale zekerheid", Weight = 0.7 },
                        new TopicKeyword { Id = 21, TopicId = 2, Name = "Beleggingen", Weight = 0.6 },
                        new TopicKeyword { Id = 22, TopicId = 2, Name = "Inflatie", Weight = 0.5 },
                        new TopicKeyword { Id = 23, TopicId = 2, Name = "Pensioenuitkeringen", Weight = 0.7 }
                    }
                },
                new Topic
                {
                    Id = 3,
                    CollaborativeSpaceId = 3,
                    Name = "Technologie en Gadgets",
                    Keywords = new List<TopicKeyword>
                    {
                        new TopicKeyword { Id = 24, TopicId = 3, Name = "Technologie", Weight = 0.9 },
                        new TopicKeyword { Id = 25, TopicId = 3, Name = "Gadgets", Weight = 0.8 },
                        new TopicKeyword { Id = 26, TopicId = 3, Name = "Smartphones", Weight = 0.8 },
                        new TopicKeyword { Id = 27, TopicId = 3, Name = "Smartphone", Weight = 0.8 },
                        new TopicKeyword { Id = 28, TopicId = 3, Name = "Tablets", Weight = 0.7 },
                        new TopicKeyword { Id = 29, TopicId = 3, Name = "Computers", Weight = 0.7 },
                        new TopicKeyword { Id = 30, TopicId = 3, Name = "Internet", Weight = 0.8 },
                        new TopicKeyword { Id = 31, TopicId = 3, Name = "Apps", Weight = 0.6 },
                        new TopicKeyword { Id = 32, TopicId = 3, Name = "Software", Weight = 0.6 },
                        new TopicKeyword { Id = 33, TopicId = 3, Name = "Online navigeren", Weight = 0.7 },
                        new TopicKeyword { Id = 34, TopicId = 3, Name = "Social media", Weight = 0.6 },
                        new TopicKeyword { Id = 35, TopicId = 3, Name = "Beveiliging", Weight = 0.6 },
                        new TopicKeyword { Id = 36, TopicId = 3, Name = "Technische ondersteuning", Weight = 0.5 }
                    }
                }
            };

            return topics;
        }
        
        public static List<TopicKeyword> GetKeywords()
        {
            var topics = GetTopics();

            var allKeywords = topics
                .SelectMany(topic => topic.Keywords)  
                .ToList();

            return allKeywords;
        }

    }
}