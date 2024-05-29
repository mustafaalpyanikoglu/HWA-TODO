using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<User>? Users { get; set; }
    public DbSet<Role>? Roles { get; set; }
    public DbSet<UserRole>? UserRoles { get; set; }
    public DbSet<Assignment>? Assignments { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}