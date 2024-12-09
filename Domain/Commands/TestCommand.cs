using Domain.Interface;
using Domain.Model;
using Domain.Service;

namespace Domain.Commands;

public class TestCommand
{
    private readonly ISeeder _seeder;
    private readonly IGroupChatRepository _groupChatRepository;
    private readonly GroupChatService _groupChatService;

    public TestCommand(ISeeder seeder, IGroupChatRepository groupChatRepository, GroupChatService groupChatService)
    {
        _seeder = seeder;
        _groupChatRepository = groupChatRepository;
        _groupChatService = groupChatService;
    }
    
    public void Test(String[] args)
    {
        List<CollaborativeSpace> groupChats = _groupChatService.GetGroupChatsByUserId(5);
        Console.Write("test");
    }
    
}