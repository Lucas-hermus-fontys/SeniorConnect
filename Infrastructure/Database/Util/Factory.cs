using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Bogus;
using Domain.Analysis;
using Domain.Enum;
using Domain.Interface;
using Domain.Model;
using Domain.Service;
using Domain.Transformer;

namespace Infrastructure.Database.Util;

public class Factory : IFactory
{
    private readonly IDatabase _database;
    private readonly DiscussionFormService _discussionFormService;

    public Factory(IDatabase database, DiscussionFormService discussionFormAnalyzer)
    {
        _database = database;
        _discussionFormService = discussionFormAnalyzer;
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
            3, 0.8, "Smartphone",
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
                "Energie houden na je 60ste?",
                "Ik merk dat ik minder energie heb dan vroeger. Zijn er gewoonten of tips die je kunnen helpen om actiever en energieker te blijven, vooral als je ouder wordt?"
            },
            {
                "Wat is de beste manier om een rollator te gebruiken?",
                "Ik ben in de winkel geweest om naar een rollator te kijken, maar ik weet niet precies of en hoe ik die zou moeten gebruiken. Zijn er dingen waar je op moet letten of tips voor het gebruik?"
            },
            {
                "Hoe kun je het beste sparen voor je pensioen?",
                "Mijn pensioen komt steeds dichterbij, maar ik vraag me af of ik genoeg gespaard heb. Heeft iemand ervaring met pensioenplanning? Wat zou je nu anders doen als je terugkijkt?"
            },
            {
                "Tips voor een gebruiksvriendelijke smartphone?",
                "Ik ben nieuw met smartphones en wil er graag meer mee doen. Welke apps of functies zijn handig voor mensen die niet zo technisch zijn, maar wel willen profiteren van de technologie?"
            },
            {
                "Wat helpt tegen ochtendstijfheid in de gewrichten?",
                "Ik merk dat ik last krijg van stijfheid, vooral 's ochtends. Hebben anderen oefeningen of routines die helpen om de pijn te verminderen en de soepelheid te verbeteren?"
            },
            {
                "Moet ik mijn pensioenfondsen diversifiëren?",
                "Met de huidige economische veranderingen begin ik te twijfelen of mijn pensioenfondsen nog wel goed verdeeld zijn. Hebben anderen hun beleggingsstrategie aangepast naarmate ze dichter bij hun pensioen kwamen?"
            },
            {
                "Zijn er goede apps voor het beheren van je gezondheid?",
                "Ik heb gehoord van apps die je helpen bij het bijhouden van je gezondheid, maar ik weet niet welke ik moet proberen. Gebruikt iemand van jullie een app voor gezondheid of fitheid die je zou aanraden?"
            },
            {
                "Hoe kan ik gezonder eten zonder veel moeite?",
                "Ik wil gezonder eten, maar koken wordt steeds lastiger. Wat zijn makkelijke manieren om gezondere keuzes te maken, vooral als het gaat om voeding voor ouderen?"
            },
            {
                "Hoe zorg je ervoor dat je financiële planning klopt?",
                "Na mijn pensioen wil ik zeker weten dat ik mijn geld goed beheer. Hoe hebben jullie ervoor gezorgd dat je financiële situatie stabiel blijft, zelfs als je minder werkt?"
            },
            {
                "Hoe kunnen sociale media je humeur beïnvloeden?",
                "Ik gebruik sociale media om in contact te blijven met familie, maar soms voel ik me er niet goed door. Heeft iemand gemerkt dat sociale media invloed heeft op je mentale gezondheid?"
            },
            {
                "Hoe veilig ben ik online?",
                "Ik maak me zorgen over mijn online veiligheid. Ik ben niet zo technisch, dus ik weet niet goed wat ik moet doen om mezelf te beschermen. Heeft iemand tips om veilig online te blijven?"
            },
            {
                "Wat zijn de beste manieren om in contact te blijven met vrienden?",
                "Nu ik wat ouder ben, merk ik dat ik mijn vrienden niet zo vaak zie. Wat doen jullie om in contact te blijven, vooral als je niet altijd in de buurt bent?"
            },
            {
                "Heeft iemand ervaring met het kiezen van een pensioenfonds?",
                "Ik wil beginnen met het kiezen van een pensioenfonds, maar het is zo ingewikkeld. Heeft iemand ervaring met het maken van deze keuze? Wat zou je aanraden?"
            },
            {
                "Beleggen in mijn pensioen: Is dat een goed idee?",
                "Ik ben van plan om mijn pensioen wat meer te beleggen, maar ik ben niet zeker of het het juiste moment is. Heeft iemand ervaring met beleggen voor je pensioen?"
            },
            {
                "Gezond blijven zonder zware oefeningen?",
                "Ik wil graag in beweging blijven, maar zware oefeningen zijn niet meer zo makkelijk voor me. Wat zijn goede alternatieven die je kunnen helpen om fit te blijven?"
            },
            {
                "Wat kan ik doen tegen vergeetachtigheid?",
                "Ik merk dat ik vaker dingen vergeet. Zijn er mentale oefeningen of strategieën die je helpen om je geheugen scherp te houden naarmate je ouder wordt?"
            },
            {
                "Welke apps zijn nuttig voor boodschappen doen?",
                "Zijn er apps die het makkelijker maken om boodschappen te doen zonder de deur uit te hoeven? Ik zou graag willen weten welke apps handig zijn om alles thuisbezorgd te krijgen."
            },
            {
                "Wat zijn goede routines om je gezondheid op peil te houden?",
                "Ik ben op zoek naar een simpele dagelijkse routine die me helpt gezond te blijven. Wat zijn kleine veranderingen die je hebt doorgevoerd om gezonder te leven?"
            },
            {
                "Sociale zekerheid en pensioen: Wat moet ik weten?",
                "Ik hoor veel over sociale zekerheid, maar ik weet niet goed hoe dat samenwerkt met mijn pensioen. Kan iemand me uitleggen hoe sociale zekerheid mijn pensioen beïnvloedt?"
            },
            {
                "Oefeningen voor flexibiliteit en pijnverlichting",
                "Hebben jullie specifieke oefeningen die je helpen om je flexibiliteit te behouden of pijn te verminderen? Vooral als je ouder wordt en meer last krijgt van stijfheid."
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

        CollaborativeSpace discussionForm = _discussionFormService.GetDiscussionFormById(newId);
        List<Topic> topics = _discussionFormService.GetTopics();
        DiscussionFormAnalyzer analyzer = new DiscussionFormAnalyzer(discussionForm, topics);
        List<Topic> relatedTopics = analyzer.GetTopicsFromContext();
        if (relatedTopics.Any())
        {
            _discussionFormService.AddTopicsToDiscussionForm(discussionForm, relatedTopics);
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
            CreateRecursiveComments(spaceId, parentId, depth + 1);
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