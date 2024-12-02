using Infrastructure.Database;

namespace Domain.Commands;

public class TestCommand
{
    private readonly Seeder _seeder;

    public TestCommand(Seeder seeder)
    {
        _seeder = seeder;
    }
    
    public void Test(String[] args)
    {
        _seeder.SeedDatabase();
    }
    
}