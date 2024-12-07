using Domain.Interface;

namespace Domain.Commands;

public class TestCommand
{
    private readonly ISeeder _seeder;

    public TestCommand(ISeeder seeder)
    {
        _seeder = seeder;
    }
    
    public void Test(String[] args)
    {
        _seeder.SeedDatabase();
    }
    
}