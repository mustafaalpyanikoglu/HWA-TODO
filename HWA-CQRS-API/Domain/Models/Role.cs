using Core.Persistence;

namespace Domain.Models;

public class Role : BaseEntity
{
    public string Name { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; }

    // Empty constructor
    public Role()
    {
        UserRoles = new HashSet<UserRole>();
    }

    // Full constructor
    public Role(int id, string name,
        DateTime createdDate, DateTime? deletedDate, DateTime? updatedDate)
    {
        Id = id;
        Name = name;
        CreatedDate = createdDate;
        DeletedDate = deletedDate;
        UpdatedDate = updatedDate;
    }
}