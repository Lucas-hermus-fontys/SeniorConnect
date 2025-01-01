using System;

namespace Domain.Model;

public class User
{
    public int Id { get; set; }
    public UserRole UserRole { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public bool Active { get; set; }
    public string GoogleId { get; set; }
    public string FacebookId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NameAffix { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string ProfilePictureUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int CollaborativeSpaceId { get; set; }
    
    public int CollaborativeSpaceMessageId { get; set; }
    
}