using Core.Persistence;

namespace Domain.Models;
public class Assignment : BaseEntity
{
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }

    // Navigation property for User
    public virtual User User { get; set; }

    public Assignment()
    {
    }

    public Assignment(int id, string title, string description, string status, 
        DateTime createdDate, DateTime? updatedDate, DateTime? deletedDate, int userId)
    {
        Id = id;
        UserId = userId;
        Title = title;
        Description = description;
        Status = status;
        CreatedDate = createdDate;
        DeletedDate = deletedDate;
        UpdatedDate = updatedDate;
    }
}