using Domain.Interface;

namespace Domain.Commands;

public class TestCommand
{
    private readonly ISeeder _seeder;
    private readonly IGroupChatRepository _groupChatRepository;

    public TestCommand(ISeeder seeder, IGroupChatRepository groupChatRepository)
    {
        _seeder = seeder;
        _groupChatRepository = groupChatRepository;
    }
    
    public void Test(String[] args)
    {
        // _seeder.SeedDatabase();
        var test = _groupChatRepository.GetGroupChatsByUserId(5);
        Console.Write("test");
    }
    
}