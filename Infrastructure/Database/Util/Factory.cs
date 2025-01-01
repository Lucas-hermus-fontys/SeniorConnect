using System;
using System.Collections.Generic;
using System.Data;
using Bogus;
using Domain.Enum;
using Domain.Interface;
using Domain.Transformer;

namespace Infrastructure.Database.Util;

public class Factory : IFactory
{
    private readonly IDatabase _database;

    public Factory(IDatabase database)
    {
        _database = database;
    }

    public void PopulateTestData()
    {
        tempTest();
        for (int i = 0; i < 10; i++)
        {
            CreateTestUser(1);
            CreateTestUser(2);
            CreateTestUser(3);
        }

        CreateTestDirectMessage();
        CreateTestTopics();
        CreateTestDiscussionForms();
    }

    private void tempTest()
    {
        Faker faker = new Faker("nl");
        Person person = faker.Person;

        _database.ExecuteQuery(
            "INSERT INTO user (role_id, email, password, salt, active, first_name, last_name, phone_number, postal_code, country, city, date_of_birth, profile_picture_url) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?);",
            2,
            person.Email,
            "f62c84f98367cb3e942cfcf3d9961c990940c389de740563447a9db2cf7930dc",
            "3063cbf1fc2a09c6394560551f89464064fb072a1854d72dfc3b8552be49a313",
            1,
            person.FirstName,
            person.LastName,
            person.Phone,
            person.Address.ZipCode,
            "The netherlands",
            person.Address.City,
            person.DateOfBirth,
            faker.Image.PicsumUrl(300, 300)
        );
        Console.WriteLine("Email of test user 1: " + person.Email + "\n");

        faker = new Faker("nl");
        person = faker.Person;

        _database.ExecuteQuery(
            "INSERT INTO user (role_id, email, password, salt, active, first_name, last_name, phone_number, postal_code, country, city, date_of_birth, profile_picture_url) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?);",
            2,
            person.Email,
            "f62c84f98367cb3e942cfcf3d9961c990940c389de740563447a9db2cf7930dc",
            "3063cbf1fc2a09c6394560551f89464064fb072a1854d72dfc3b8552be49a313",
            1,
            person.FirstName,
            person.LastName,
            person.Phone,
            person.Address.ZipCode,
            "The netherlands",
            person.Address.City,
            person.DateOfBirth,
            faker.Image.PicsumUrl(300, 300)
        );
        Console.WriteLine("Email of test user 2: " + person.Email + "\n");
    }

    private void CreateTestUser(int roleId)
    {
        Faker faker = new Faker("nl");
        Person person = faker.Person;

        _database.ExecuteQuery(
            "INSERT INTO user (role_id, email, password, salt, active, first_name, last_name, phone_number, postal_code, country, city, date_of_birth, profile_picture_url) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?);",
            roleId,
            person.Email,
            "f62c84f98367cb3e942cfcf3d9961c990940c389de740563447a9db2cf7930dc",
            "3063cbf1fc2a09c6394560551f89464064fb072a1854d72dfc3b8552be49a313",
            1,
            person.FirstName,
            person.LastName,
            person.Phone,
            person.Address.ZipCode,
            "The netherlands",
            person.Address.City,
            person.DateOfBirth,
            // faker.Image.LoremFlickrUrl(200, 200, "people")
            faker.Image.PicsumUrl(300, 300)
        );
    }

    private void CreateTestTopics()
    {
        _database.ExecuteQuery("INSERT INTO topic (name) VALUES (?),(?),(?);",
            "Gezondheid en Welzijn",
            "Pensioenplanning",
            "Technologie en Gadgets"
        );

        _database.ExecuteQuery(
            "INSERT INTO topic_keyword (topic_id, weight, name) VALUES (?,?,?),(?,?,?),(?,?,?),(?,?,?),(?,?,?),(?,?,?),(?,?,?),(?,?,?),(?,?,?),(?,?,?),(?,?,?),(?,?,?);",
            1, 0.9, "Gezondheid",
            1, 0.8, "Welzijn",
            1, 0.8, "Chronische ziektes",
            1, 0.7, "Medicatie",
            1, 0.7, "Voeding",
            1, 0.6, "Oefenroutines",
            1, 0.8, "Mentale gezondheid",
            1, 0.6, "Gezondheidszorg",
            1, 0.6, "Therapie",
            1, 0.6, "Lichaamsbeweging",
            1, 0.6, "Preventie",
            1, 0.5, "Levensstijl"
        );

        _database.ExecuteQuery(
            "INSERT INTO topic_keyword (topic_id, weight, name) VALUES (?,?,?),(?,?,?),(?,?,?),(?,?,?),(?,?,?),(?,?,?),(?,?,?),(?,?,?),(?,?,?),(?,?,?),(?,?,?);",
            2, 0.9, "Pensioen",
            2, 0.8, "Financiële planning",
            2, 0.7, "Investeringen",
            2, 0.8, "Pensioenfondsen",
            2, 0.8, "Financiële zekerheid",
            2, 0.7, "Spaargeld",
            2, 0.6, "Pensioenleeftijd",
            2, 0.7, "Sociale zekerheid",
            2, 0.6, "Beleggingen",
            2, 0.5, "Inflatie",
            2, 0.7, "Pensioenuitkeringen"
        );

        _database.ExecuteQuery(
            "INSERT INTO topic_keyword (topic_id, weight, name) VALUES (?,?,?),(?,?,?),(?,?,?),(?,?,?),(?,?,?),(?,?,?),(?,?,?),(?,?,?),(?,?,?),(?,?,?),(?,?,?),(?,?,?);",
            3, 0.9, "Technologie",
            3, 0.8, "Gadgets",
            3, 0.8, "Smartphones",
            3, 0.7, "Tablets",
            3, 0.7, "Computers",
            3, 0.8, "Internet",
            3, 0.6, "Apps",
            3, 0.6, "Software",
            3, 0.7, "Online navigeren",
            3, 0.6, "Social media",
            3, 0.6, "Beveiliging",
            3, 0.5, "Technische ondersteuning"
        );
    }

    private void CreateTestDirectMessage()
    {
        DataTable users =
            _database.ExecuteQuery("SELECT * FROM user WHERE role_id = 2 and id not in (1, 2, 3) ORDER BY id LIMIT 2;");
        _database.ExecuteQuery(
            "INSERT INTO collaborative_space (name, type, is_direct_message, is_active, description, image_url, created_at, updated_at) VALUES (?, ?, ?, ?, ?, ?, ?, ?);",
            null, CollaborativeSpaceType.CHAT.ToString(), true, true, null, null, DateTime.Now, null
        );

        _database.ExecuteQuery(
            "INSERT INTO collaborative_space_user (user_id, collaborative_space_id, is_creator) VALUES (?, ?, ?),(?, ?, ?);",
            4, 1, false,
            5, 1, false
        );

        _database.ExecuteQuery(
            "INSERT INTO collaborative_space_message (user_id, collaborative_space_id, message, is_active, created_at) VALUES (?, ?, ?, ?, ?),(?, ?, ?, ?, ?),(?, ?, ?, ?, ?),(?, ?, ?, ?, ?),(?, ?, ?, ?, ?),(?, ?, ?, ?, ?),(?, ?, ?, ?, ?),(?, ?, ?, ?, ?);",
            4, 1, new Faker("nl").Lorem.Sentence(), true, DateTime.Now,
            5, 1, new Faker("nl").Lorem.Sentence(), true, DateTime.Now,
            4, 1, new Faker("nl").Lorem.Sentence(), true, DateTime.Now,
            5, 1, new Faker("nl").Lorem.Sentence(), true, DateTime.Now,
            5, 1, new Faker("nl").Lorem.Sentence(), true, DateTime.Now,
            4, 1, new Faker("nl").Lorem.Sentence(), true, DateTime.Now,
            5, 1, new Faker("nl").Lorem.Sentence(), true, DateTime.Now,
            4, 1, new Faker("nl").Lorem.Sentence(), true, DateTime.Now,
            4, 1, new Faker("nl").Lorem.Sentence(), true, DateTime.Now
        );
    }

    private void CreateTestDiscussionForms()
    {
        var discussionForms = new Dictionary<string, string>
        {
            {
                "Heeft iemand ervaring met het gebruik van een rollator?",
                "Ik heb onlangs een rollator aangeschaft omdat ik wat moeite heb met lopen. Heeft iemand anders tips over het gebruik van een rollator?"
            },
            {
                "Wie herinnert zich nog de oude televisies met draaiknoppen?",
                "Ik zat laatst te denken aan de tijd toen je de tv moest afstemmen met een draaiknop. Wat vonden jullie daarvan?"
            },
            {
                "Hoe houd ik mijn geheugen scherp naarmate ik ouder word?",
                "Ik merk dat ik soms wat vergeetachtig ben. Heeft iemand tips om mijn geheugen te verbeteren?"
            },
            {
                "Zijn er leuke spelletjes om te doen met mijn kleinkinderen?",
                "Mijn kleinkinderen komen regelmatig op bezoek. Iemand tips voor leuke, eenvoudige spelletjes?"
            },
            {
                "Wat kan ik doen tegen koude voeten in de winter?",
                "Ik heb altijd last van koude voeten in de winter. Heeft iemand een goede tip om ze warm te houden?"
            },
            {
                "Wie herinnert zich nog de goede oude tijden van de postbode?",
                "Vroeger was de postbode altijd een vertrouwd gezicht. Wie heeft herinneringen aan die tijd?"
            },
            {
                "Hoe kan ik mijn tuin het beste onderhouden zonder veel te bukken?",
                "Ik hou van tuinieren, maar bukken wordt moeilijker. Heeft iemand tips om mijn tuin goed te onderhouden?"
            },
            {
                "Wat zijn goede manieren om in contact te blijven met familie?",
                "Ik wil graag in contact blijven met mijn familie. Iemand tips voor eenvoudige manieren om te bellen of videobellen?"
            },
            {
                "Wie heeft er nog een platenspeler?",
                "Ik luister af en toe naar vinyl. Heeft iemand anders ook nog een platenspeler?"
            },
            {
                "Wat is een goed recept voor een lichte maaltijd als je minder eet?",
                "Ik eet tegenwoordig wat minder. Heeft iemand een goed recept voor een lichte, voedzame maaltijd?"
            },
            {
                "Wat zijn de voordelen van wandelen voor oudere mensen?",
                "Ik probeer dagelijks een stukje te wandelen. Wat zijn de voordelen van wandelen op latere leeftijd?"
            },
            {
                "Wie heeft er ervaring met het gebruik van een medicijnwekker?",
                "Ik vergeet soms mijn medicijnen. Heeft iemand ervaring met een medicijnwekker?"
            },
            {
                "Wat kan ik doen tegen pijn in mijn gewrichten?",
                "Mijn gewrichten doen pijn, vooral in de winter. Heeft iemand tips voor verlichting?"
            },
            {
                "Zijn er nog gezellige activiteiten in de buurt voor ouderen?",
                "Ik ben op zoek naar leuke activiteiten voor ouderen in de buurt. Iemand suggesties?"
            },
            {
                "Wie heeft er ervaring met het gebruik van een fiets met trapondersteuning?",
                "Mijn kinderen raden een fiets met trapondersteuning aan. Heeft iemand ervaring hiermee?"
            },
            {
                "Wat kan ik doen om mijn handen soepel te houden?",
                "Mijn handen zijn vaak stijf. Heeft iemand tips om ze soepel te houden?"
            },
            {
                "Heeft iemand tips voor het verbeteren van mijn gehoor?",
                "Ik heb gemerkt dat mijn gehoor de laatste tijd achteruit gaat. Heeft iemand suggesties voor hulpmiddelen of oefeningen?"
            },
            {
                "Wat zijn de beste manieren om mijn gewicht te beheersen?",
                "Ik probeer gezonder te eten en wat kilo's kwijt te raken. Heeft iemand tips of ervaringen met gewichtsbeheersing?"
            },
            {
                "Wie herinnert zich nog de oude winkels in ons dorp?",
                "Vroeger hadden we zoveel leuke winkels in ons dorp. Heeft iemand herinneringen aan de tijd dat deze winkels nog actief waren?"
            },
            {
                "Wat zijn jullie favoriete boeken om te lezen?",
                "Ik lees graag, maar soms weet ik niet welk boek ik nu moet pakken. Heeft iemand een goed boek aan te bevelen?"
            },
            {
                "Wat is een goed idee voor een hobby die niet veel inspanning vereist?",
                "Ik ben op zoek naar een nieuwe hobby die niet te veel fysieke inspanning vereist. Iemand suggesties?"
            },
            {
                "Hoe kan ik mijn sociale contacten onderhouden?",
                "Nu mijn kinderen en vrienden verder weg wonen, vraag ik me af hoe ik mijn sociale contacten beter kan onderhouden. Heeft iemand tips?"
            },
            {
                "Wat kan ik doen om mijn huis veilig te maken?",
                "Ik wil mijn huis wat veiliger maken, vooral met betrekking tot struikelgevaar. Heeft iemand tips voor het verbeteren van de veiligheid?"
            },
            {
                "Hoe kan ik de trap gemakkelijker opkomen?",
                "De trap is voor mij een beetje een uitdaging geworden. Heeft iemand tips of hulpmiddelen die het makkelijker maken?"
            },
            {
                "Wat is jullie favoriete manier om te ontspannen?",
                "Na een lange dag zoek ik manieren om te ontspannen. Wat doen jullie om tot rust te komen?"
            },
            {
                "Wie heeft er ervaring met meditatie voor ouderen?",
                "Ik ben benieuwd of meditatie kan helpen om tot rust te komen. Heeft iemand ervaring met meditatie of ademhalingsoefeningen?"
            },
            {
                "Wat kunnen we doen om te helpen bij het geheugenverlies?",
                "Ik merk dat mijn geheugen niet meer is wat het was. Heeft iemand tips of technieken om het geheugen scherp te houden?"
            },
            {
                "Wie heeft er ervaring met het gebruik van een tablet of smartphone?",
                "Ik ben nieuw met een tablet en wil graag weten hoe ik het optimaal kan gebruiken. Heeft iemand hier ervaring mee?"
            },
            {
                "Wat is een goed idee voor een lichte wandeling in de buurt?",
                "Ik wil elke dag een wandeling maken, maar zoek een rustige route. Heeft iemand suggesties voor een goede wandelroute in de buurt?"
            },
            {
                "Hoe kan ik mijn energie het hele dag door behouden?",
                "Soms voel ik me uitgeput gedurende de dag. Heeft iemand tips om mijn energie beter te verdelen?"
            },
            {
                "Wat is het beste voor je gezondheid: yoga of pilates?",
                "Ik ben geïnteresseerd in het proberen van een van deze activiteiten, maar weet niet welke het beste is voor mijn gezondheid. Heeft iemand ervaring met yoga of pilates?"
            },
            {
                "Wie heeft er ervaring met een verhoogd bed?",
                "Mijn bed is de laatste tijd wat moeilijker om in en uit te komen. Heeft iemand ervaring met een verhoogd bed?"
            },
            {
                "Wat kan ik doen om mijn huid gezond te houden?",
                "Mijn huid lijkt droger te worden naarmate ik ouder word. Heeft iemand tips voor het behouden van een gezonde huid?"
            },
            {
                "Wat is het beste om te doen tegen last van de knie?",
                "Mijn knieën beginnen pijn te doen, vooral na een wandeling. Heeft iemand tips of oefeningen die helpen bij knieklachten?"
            },
            {
                "Wie heeft er ervaring met het aanpassen van de verlichting in huis?",
                "Ik merk dat het in huis vaak te donker is. Heeft iemand ervaring met het aanpassen van de verlichting, zoals het gebruik van dimmers of andere lampen?"
            },
            {
                "Wat zijn goede manieren om actief te blijven zonder te veel te belasten?",
                "Ik wil actief blijven zonder mijn lichaam teveel te belasten. Heeft iemand suggesties voor activiteiten die niet te zwaar zijn?"
            }
        };

        foreach (var entry in discussionForms)
        {
            CreateTestDiscussionForm(entry.Key, entry.Value);
        }
    }

    private void CreateTestDiscussionForm(string title, string body)
    {
        Random random = new Random();

        _database.ExecuteQuery(
            "INSERT INTO collaborative_space (name, type, is_direct_message, is_active, description, created_at, updated_at) VALUES (?, ?, ?, ?, ?, ?, ?);",
            title, CollaborativeSpaceType.FORM.ToString(), false, true, body, DateTime.Now, null
        );

        int newId =
            (int)_database.ExecuteQuery("SELECT * FROM collaborative_space ORDER BY ID DESC LIMIT 1;").Rows[0]["id"];

        _database.ExecuteQuery(
            "INSERT INTO collaborative_space_user (user_id, collaborative_space_id, is_creator) VALUES (?, ?, ?);",
            random.Next(4, 36), newId, true
        );

        int rng = new Random().Next(0, 5);
        
        Console.WriteLine("----------------------------------------------------------------------------");
        
        
        for (int i = 0; i < rng; i++)
        {
            CreateRecursiveComments(newId, null);
            Console.WriteLine("Currently at i = " + i + " of : " + rng);
        }
    }

    private void CreateRecursiveComments(int spaceId, string parentId, double depth = 0)
    {
        List<string> forumComments = new List<string>
        {
            "I love this idea! I used to enjoy knitting scarves for everyone.",
            "It’s so nice to read everyone’s stories. I feel like I’m getting to know you all.",
            "I’ve been thinking a lot about my childhood lately. I remember when we used to gather around the fireplace, telling stories before bed. Those were the simple times.",
            "The weather’s been a bit chilly lately. I try to bundle up with a warm cup of tea and watch the birds from my window. It’s the little things that bring joy.",
            "Oh, I remember family picnics so well! My mom used to pack everything in baskets, and we’d spend the whole day by the lake. Those were the days when life was slower, and you didn’t need much to be happy.",
            "I’ve been keeping myself busy with puzzles. I can’t do them as fast as I used to, but it’s a good way to pass the time and keep the mind sharp!",
            "I can’t believe how fast time flies. I was just thinking about how much I miss the old family reunions. The laughter, the games, the food. Such wonderful memories!",
            "The other day, I went through an old photo album. It brought back so many memories of when my children were little. I almost forgot how much joy they brought me. Now, they’re all grown up with families of their own. Time really does fly.",
            "I miss the days when we would get together and play cards late into the night. Those were some of the happiest moments of my life. Maybe we should organize a game night here?",
            "I’ve been staying busy by going for walks and enjoying the beauty of the garden. The fresh air always helps me clear my head.",
            "I don’t do much knitting anymore, but I remember when I used to make blankets for everyone. It felt so good to create something with my hands. I wonder if I should start again.",
            "What a lovely discussion! I’ve been thinking about how much joy can come from the smallest of things. The other day, I spent an afternoon reading old letters from my friends. It made me feel so connected to them again.",
            "I used to love the old radio shows when I was younger. They were such a fun way to unwind. I wish we could have something like that again. Does anyone else miss those?",
            "I have to say, it’s nice to hear everyone's memories. I’ve lived a full life, but sometimes it feels like it’s all just a blur. It’s only when I start talking about it that I realize how much I’ve experienced.",
            "It’s so refreshing to hear from everyone. I feel like we are all part of a little community here, sharing our lives and memories. It’s comforting, especially on days when it feels quiet.",
            "I’m not as quick as I used to be with my knitting, but I still enjoy it. There’s something so peaceful about it, just sitting with yarn and needles and letting my mind wander.",
            "Do any of you enjoy writing letters? I used to write so many letters to my family when I was younger. It’s a nice way to stay in touch, even now.",
            "I’ve been feeling nostalgic lately. I found some old letters from my childhood friends, and it made me smile to remember those carefree days. It’s nice to have those memories to look back on.",
            "I’ve always loved gardening. Even though I don’t do it as much now, I still enjoy seeing the flowers bloom. The scent of fresh flowers is one of life’s little pleasures.",
            "Life moves at such a different pace now. It’s funny, isn’t it? How everything slows down, but in a way, it’s nice. I enjoy the quiet moments more now than I ever did before.",
            "I’ve been thinking a lot about the past lately, and how I wish I had written more down. There’s so much to remember, and sometimes it’s hard to keep track of it all. Maybe it’s never too late to start a journal?",
            "I can’t believe how much has changed over the years. The world today is so different from when I was younger, but I try to focus on the positives. It’s comforting to know that we’re all here for each other.",
            "I miss the good old days when we could all gather together, play games, and just enjoy each other’s company. There was always so much laughter. I wish we could do that again.",
            "My daughter came by yesterday to visit. We spent the afternoon talking about family history and going through old albums. It was such a nice afternoon. It reminded me of how important family is.",
            "I remember when I used to bake every Sunday. My kitchen always smelled of fresh bread and cookies. I miss that smell, and I miss the family being there. Maybe I’ll try baking again soon.",
            "I always feel a little better after spending time in the garden. It’s so peaceful, just watching the flowers grow and listening to the birds. It makes everything feel right again.",
            "I know what you mean. I used to have a big vegetable garden, but now I just enjoy watching the flowers in the courtyard. It’s still a little bit of nature, and it always brightens my day.",
            "The little things in life have become more important as I’ve gotten older. A warm cup of tea, a good book, and a chat with a friend – these things bring so much happiness. It’s comforting to know we all appreciate these simple joys.",
            "I’m always so impressed by how strong and resilient everyone is here. Life hasn’t always been easy, but we’re all here supporting each other, and that makes all the difference.",
            "I’ve been meaning to pick up some new hobbies. Maybe I should try painting or drawing. I used to enjoy art when I was younger. It would be nice to start again.",
            "I’ve been thinking about all the wonderful trips I’ve taken in my life. There was one trip to the beach that stands out – it was just the perfect day. I think I’ll try to find the pictures and share them here.",
            "I’ve heard that taking up new hobbies helps keep the mind sharp. I might start doing more puzzles. I used to love doing them with my kids.",
            "Sometimes I look back on my life, and I’m amazed at how much I’ve learned. It’s funny, the older you get, the more you realize that you never stop learning.",
            "I’ve been thinking about how life is like a series of little moments. It’s those small, quiet moments that really shape our memories, and they’re the ones we hold on to.",
        };

        CreateTestComment(forumComments[new Random().Next(forumComments.Count)], spaceId, parentId);

        if (new Random().NextDouble() > 0.5 + depth)
        {
            CreateRecursiveComments(spaceId, parentId, depth + 0.01);
        }
        
        if (new Random().NextDouble() > 0.3)
        {
            return;
        }

        int newParentId = (int)_database.ExecuteQuery("SELECT id FROM collaborative_space_message ORDER BY ID DESC LIMIT 1;").Rows[0]["id"];

        CreateRecursiveComments(spaceId, newParentId.ToString());
    }


    private void CreateTestComment(string message, int space_id, string parentId = null)
    {
        Random random = new Random();

        _database.ExecuteQuery(
            "INSERT INTO collaborative_space_message (user_id, parent_id, collaborative_space_id, message, is_active, created_at) VALUES (?, ?, ?, ?, ?, ?);",
            random.Next(4, 36), parentId, space_id, message, true, DateTime.Now
        );
    }
}