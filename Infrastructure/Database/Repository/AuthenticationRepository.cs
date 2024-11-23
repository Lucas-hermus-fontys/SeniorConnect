using Infrastructure.Util;

namespace Infrastructure.Database.Repository;

public class AuthenticationRepository
{
    private readonly Database _databaseUtil;

    public AuthenticationRepository()
    {
        _databaseUtil = ServiceLocator.GetService<Database>();
    }
}