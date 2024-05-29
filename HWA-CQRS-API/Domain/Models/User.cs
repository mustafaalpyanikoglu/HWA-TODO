using Core.Persistence;

namespace Domain.Models;

public class User : BaseEntity
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; }
    public virtual ICollection<Assignment> Assignments { get; set; }

    // Empty constructor
    public User()
    {
        UserRoles = new HashSet<UserRole>();
        Assignments = new HashSet<Assignment>();
    }

    // Full constructor
    public User(int id, string username, string password, 
        string email, DateTime createdDate, DateTime? deletedDate, DateTime? updatedDate)
    {
        Id = id;
        Username = username;
        Password = password;
        Email = email;
        CreatedDate = createdDate;
        DeletedDate = deletedDate;
        UpdatedDate = updatedDate;
    }
}