using Core.Persistence;

namespace Domain.Models;

public class UserRole : BaseEntity
{
    public int UserId { get; set; }
    public int RoleId { get; set; }

    // Navigation properties
    public virtual User User { get; set; }
    public virtual Role Role { get; set; }

    public UserRole()
    {
    }

    public UserRole(int id, int userId, int roleId,
        DateTime createdDate, DateTime? deletedDate, DateTime? updatedDate) : this()
    {
        Id = id;
        UserId = userId;
        RoleId = roleId;
        CreatedDate = createdDate;
        DeletedDate = deletedDate;
        UpdatedDate = updatedDate;
    }
}