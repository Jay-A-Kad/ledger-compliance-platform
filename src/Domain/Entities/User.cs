

namespace LedgerPlatform.Domain.Entities;

public enum UserRole
{
    Customer,
    Teller,
    Auditor,
    Admin
};

public class User
{
    
    public Guid Id { get; private set; }

    public Guid ExternalId { get; private set; }
    public string Name { get;  private set; } 
    public string Email { get;  private set; } 
    public UserRole Role { get; private set; }

    public User(Guid id, Guid externalId, string name, string email, UserRole role)
    {

        if(string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));
        }
        if(string.IsNullOrWhiteSpace(email)){
            throw new ArgumentException("Email cannot be null or empty.", nameof(email));
        }


        Id = id;
        ExternalId = externalId;
        Name = name;
        Email = email;
        Role = role;
    }


    //update user profile
    public void UpdateProfile(string name, string email)
    {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            }
            if(string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));
            }
            Name = name;
            Email = email;
    }

    public void RefreshRoleCache(UserRole newRole)
    {
        Role = newRole;
    }

}