using Domain.Interface;

namespace Infrastructure.Database.Repository;

public class AuthenticationRepository : IAuthenticationRepository
{
    private readonly IDatabase _database;

    public AuthenticationRepository(IDatabase database)
    {
        _database = database;
    }
}