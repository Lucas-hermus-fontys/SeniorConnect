using Infrastructure.Database;

namespace Domain.Commands;

public class TestCommand
{
    private readonly Seeder _seeder = new();
    public void Test(String[] args)
    {
        _seeder.SeedDatabase();
    }
    
}