using Core.Persistence;

namespace Domain.Models;

public class User : BaseEntity
{
    public string Username { get; set; }
    public string Email { get; set; } 
    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; }
    public virtual ICollection<Assignment> Assignments { get; set; }

    public User()
    {
        UserRoles = new HashSet<UserRole>();
        Assignments = new HashSet<Assignment>();
    }

    public User(int id, string username, byte[] passwordSalt, byte[] passwordHash, 
        string email, DateTime createdDate, DateTime? deletedDate, DateTime? updatedDate)
    {
        Id = id;
        Username = username;
        Email = email;
        PasswordSalt = passwordSalt;
        PasswordHash = passwordHash;
        CreatedDate = createdDate;
        DeletedDate = deletedDate;
        UpdatedDate = updatedDate;
    }
}