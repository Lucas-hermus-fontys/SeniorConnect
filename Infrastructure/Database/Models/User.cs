using System;

namespace Infrastructure.Database.Models
{
    public record User(
        UserRole UserRole,
        string Email,
        string Password,
        string Salt,
        bool Active
    );
}