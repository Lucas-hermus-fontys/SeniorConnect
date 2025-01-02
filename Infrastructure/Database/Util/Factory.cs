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
            title, CollaborativeSpaceType.FORM.ToString(), false, true, body, GenerateRandomDateWithinTwoWeeks(), null
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
            "Ik hou van dit idee! Vroeger breide ik graag sjaals voor iedereen.",
            "Het is zo fijn om de verhalen van iedereen te lezen. Ik voel me alsof ik jullie allemaal leer kennen.",
            "Ik heb de laatste tijd veel nagedacht over mijn kindertijd. Ik herinner me nog dat we altijd bij de open haard zaten, verhalen vertelden voor het slapen gaan. Dat waren de simpele tijden.",
            "Het weer is de laatste tijd een beetje koud. Ik probeer me warm in te pakken met een kop thee en kijk naar de vogels vanuit mijn raam. Het zijn de kleine dingen die vreugde brengen.",
            "Oh, ik herinner me de familiepicknicks zo goed! Mijn moeder pakte altijd alles in manden, en we brachten de hele dag aan het meer door. Dat waren de dagen waarin het leven trager ging, en je had niet veel nodig om gelukkig te zijn.",
            "Ik houd mezelf bezig met puzzels. Ik kan ze niet zo snel maken als vroeger, maar het is een goede manier om de tijd door te brengen en de geest scherp te houden!",
            "Ik kan niet geloven hoe snel de tijd vliegt. Ik dacht net nog aan hoe ik de oude familie reunies mis. Het lachen, de spellen, het eten. Wat een geweldige herinneringen!",
            "De andere dag doorbladerde ik een oud fotoalbum. Het bracht zoveel herinneringen terug van toen mijn kinderen nog klein waren. Ik was bijna vergeten hoeveel vreugde ze me gaven. Nu zijn ze allemaal volwassen met hun eigen gezinnen. De tijd vliegt echt.",
            "Ik mis de dagen dat we samenkwamen en kaarten speelden tot diep in de nacht. Dat waren enkele van de gelukkigste momenten van mijn leven. Misschien moeten we hier een spelavond organiseren?",
            "Ik ben bezig door wandelingen te maken en te genieten van de schoonheid van de tuin. De frisse lucht helpt altijd mijn hoofd te verhelderen.",
            "Ik brei niet zoveel meer, maar ik herinner me nog dat ik dekens maakte voor iedereen. Het voelde zo goed om iets met mijn handen te creëren. Misschien moet ik weer beginnen.",
            "Wat een leuke discussie! Ik heb de laatste tijd nagedacht over hoeveel vreugde de kleinste dingen kunnen brengen. De andere dag bracht ik een middag door met het lezen van oude brieven van mijn vrienden. Het maakte me zo verbonden met hen.",
            "Ik hield vroeger zo van de oude radioshows toen ik jonger was. Het was zo’n leuke manier om tot rust te komen. Ik wou dat we zoiets weer hadden. Mis iemand anders die ook?",
            "Ik moet zeggen dat het fijn is om ieders herinneringen te horen. Ik heb een vol leven gehad, maar soms voelt het alsof alles slechts een vage herinnering is. Pas als ik erover begin te praten, realiseer ik me hoeveel ik heb ervaren.",
            "Het is zo verfrissend om van iedereen te horen. Ik voel me alsof we allemaal deel uitmaken van een kleine gemeenschap hier, waarin we onze levens en herinneringen delen. Het is geruststellend, vooral op dagen waarop het stil lijkt.",
            "Ik ben niet zo snel meer met mijn breien, maar ik geniet er nog steeds van. Er is iets zo vredigs aan breien, gewoon zitten met garen en naalden en mijn gedachten laten afdwalen.",
            "Geniet iemand van het schrijven van brieven? Vroeger schreef ik zoveel brieven naar mijn familie toen ik jonger was. Het is een fijne manier om in contact te blijven, zelfs nu.",
            "Ik voel me de laatste tijd nostalgisch. Ik vond enkele oude brieven van mijn kindervrienden, en het deed me glimlachen om die zorgeloze dagen te herinneren. Het is fijn om die herinneringen te hebben om op terug te kijken.",
            "Ik heb altijd van tuinieren gehouden. Ook al doe ik het nu minder, ik geniet nog steeds van het zien van de bloemen bloeien. De geur van verse bloemen is een van de kleine geneugten van het leven.",
            "Het leven beweegt zich nu op een heel ander tempo. Het is grappig, niet? Hoe alles vertraagt, maar op een manier is het fijn. Ik geniet nu meer van de stille momenten dan ooit tevoren.",
            "Ik heb de laatste tijd veel nagedacht over het verleden, en hoe ik vaker dingen had moeten opschrijven. Er is zoveel te herinneren, en soms is het moeilijk om alles bij te houden. Misschien is het nooit te laat om een dagboek te beginnen?",
            "Ik kan niet geloven hoeveel er veranderd is door de jaren heen. De wereld van vandaag is zo anders dan toen ik jonger was, maar ik probeer me te concentreren op de positieve dingen. Het is geruststellend te weten dat we er allemaal voor elkaar zijn.",
            "Ik mis de goede oude tijd toen we allemaal samen konden komen, spelletjes spelen, en gewoon genieten van elkaars gezelschap. Er was altijd zoveel lachen. Ik wou dat we dat weer konden doen.",
            "Mijn dochter kwam gisteren langs op bezoek. We brachten de middag door met praten over de familiegeschiedenis en door oude albums te bladeren. Het was zo’n fijne middag. Het herinnerde me eraan hoe belangrijk familie is.",
            "Ik herinner me nog dat ik elke zondag bakte. Mijn keuken rook altijd naar vers brood en koekjes. Ik mis die geur, en ik mis de familie die daar was. Misschien ga ik weer eens bakken.",
            "Ik voel me altijd een beetje beter na tijd doorbrengen in de tuin. Het is zo vredig, gewoon kijken naar de bloemen die groeien en luisteren naar de vogels. Het maakt alles weer goed.",
            "Ik weet wat je bedoelt. Ik had vroeger een grote groentetuin, maar nu geniet ik gewoon van de bloemen in de binnenplaats. Het is nog steeds een beetje natuur, en het maakt mijn dag altijd beter.",
            "De kleine dingen in het leven zijn belangrijker geworden naarmate ik ouder ben. Een warme kop thee, een goed boek, en een gesprek met een vriend – deze dingen brengen zoveel geluk. Het is geruststellend te weten dat we allemaal deze simpele vreugden waarderen.",
            "Ik ben altijd zo onder de indruk van hoe sterk en veerkrachtig iedereen hier is. Het leven is niet altijd makkelijk geweest, maar we zijn hier allemaal elkaar aan het steunen, en dat maakt het verschil.",
            "Ik ben van plan om nieuwe hobby’s op te pakken. Misschien moet ik schilderen of tekenen proberen. Ik genoot van kunst toen ik jonger was. Het zou fijn zijn om weer te beginnen.",
            "Ik heb nagedacht over alle geweldige reizen die ik in mijn leven heb gemaakt. Er was één reis naar het strand die opvalt – het was gewoon de perfecte dag. Ik denk dat ik de foto’s ga zoeken en ze hier delen.",
            "Ik heb gehoord dat het oppakken van nieuwe hobby’s helpt om de geest scherp te houden. Misschien ga ik meer puzzels doen. Ik hield ervan om ze vroeger te doen met mijn kinderen.",
            "Soms kijk ik terug op mijn leven, en ik ben verbaasd over hoeveel ik heb geleerd. Het is grappig, hoe ouder je wordt, hoe meer je realiseert dat je nooit stopt met leren.",
            "Ik heb nagedacht over hoe het leven een reeks kleine momenten is. Het zijn die kleine, stille momenten die onze herinneringen echt vormen, en dat zijn de momenten die we vasthouden.",
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

        int newParentId =
            (int)_database.ExecuteQuery("SELECT id FROM collaborative_space_message ORDER BY ID DESC LIMIT 1;")
                .Rows[0]["id"];

        CreateRecursiveComments(spaceId, newParentId.ToString());
    }


    private void CreateTestComment(string message, int space_id, string parentId = null)
    {
        Random random = new Random();

        _database.ExecuteQuery(
            "INSERT INTO collaborative_space_message (user_id, parent_id, collaborative_space_id, message, is_active, created_at) VALUES (?, ?, ?, ?, ?, ?);",
            random.Next(4, 36), parentId, space_id, message, true, GenerateRandomDateWithinTwoWeeks()
        );
    }


    public static DateTime GenerateRandomDateWithinTwoWeeks()
    {
        Random random = new Random();
        double randomFactor = random.NextDouble();
        double adjustedFactor = Math.Pow(randomFactor, 2);
        double totalMinutesInTwoWeeks = TimeSpan.FromDays(14).TotalMinutes;
        double randomMinutes = adjustedFactor * totalMinutesInTwoWeeks;
        DateTime randomDate = DateTime.Now.AddMinutes(-1 * (int)randomMinutes);
        return randomDate;
    }
}