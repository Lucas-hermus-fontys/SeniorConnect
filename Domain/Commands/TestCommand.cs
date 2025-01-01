using Domain.Analysis;
using Domain.Enum;
using Domain.Interface;
using Domain.Model;
using Domain.Service;

namespace Domain.Commands;

public class TestCommand
{
    private readonly ISeeder _seeder;
    private readonly IGroupChatRepository _groupChatRepository;
    private readonly GroupChatService _groupChatService;
    private readonly IDiscussionFormRepository _discussionFormRepository;

    public TestCommand(ISeeder seeder, IGroupChatRepository groupChatRepository, GroupChatService groupChatService, IDiscussionFormRepository discussionformrepository)
    {
        _seeder = seeder;
        _groupChatRepository = groupChatRepository;
        _groupChatService = groupChatService;
        _discussionFormRepository = discussionformrepository;
    }

    public void Test(String[] args)
    {
        CollaborativeSpace space = new CollaborativeSpace
        {
            Type = CollaborativeSpaceType.FORM, Name = "Mijn mobiel doet het niet",
            Description =
                "Ik snap echt niks van de technologie van Technologie tegenwoordig zitten al die jong lui op hun mobiel en doen niks anders. het werkt echt voor geen meter ook nog. al die Gadgets kunnnen me een worst wezen. Het welzijn van de mensheid hangt er vanaf als je het mij vraagt."
        };
        
        List<Topic> topics = _discussionFormRepository.GetTopics();
        List<TopicKeyword> keywords = _discussionFormRepository.GetKeywords();
        topics.ForEach(topic => { if (topic.Keywords == null) topic.Keywords = new List<TopicKeyword>(); topic.Keywords.AddRange(keywords.Where(keyword => keyword.TopicId == topic.Id)); });
        
        DiscussionFormAnalyzer test = new DiscussionFormAnalyzer(space, topics);
        
 
        List<Topic> actualTopics = test.GetTopicsFromContext();

    }
}