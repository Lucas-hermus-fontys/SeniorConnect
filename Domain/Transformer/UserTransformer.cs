using System.Data;
using Domain.Model;

namespace Domain.Transformer;

public class UserTransformer
{
    public static User DataRowToUser(DataRow dataRow)
    {
        return new User
        {
            UserRole = new UserRole { Id = (int) dataRow["role_id"]},
            Email = dataRow["email"] as string,
            Salt = dataRow["salt"] as string,
            Password = dataRow["password"] as string,
            Id = (int) dataRow["id"],
            Active = (bool) dataRow["active"],
            City = dataRow["city"] as string,
            Country = dataRow["country"] as string,
            DateOfBirth = dataRow["date_of_birth"] as DateTime? ?? DateTime.MinValue,
            FirstName = dataRow["first_name"] as string,
            LastName = dataRow["last_name"] as string,
            NameAffix = dataRow["name_affix"] as string,
            PhoneNumber = dataRow["phone_number"] as string,
            PostalCode = dataRow["postal_code"] as string,
            ProfilePictureUrl = dataRow["profile_picture_url"] as string
        };
    }
}