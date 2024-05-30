
using Core.Persistence.Repositories;
using Domain.Models;
using Persistence.Contexts;

namespace Application.Services.UserService;

public class UserManager(IRepository<User, AppDbContext> userRepository) : IUserService
{
    private readonly IRepository<User, AppDbContext> _userRepository = userRepository;
    
    public async Task<User?> GetByEmail(string email)
    {
        var user = await _userRepository.GetSingleOrDefaultAsync(u => u.Email == email);
        return user;
    }

    public async Task<User> GetById(int id)
    {
        var user = await _userRepository.GetSingleOrDefaultAsync(u => u.Id == id);
        return user;
    }

    public async Task<User> Update(User user)
    {
        var updatedUser = await _userRepository.UpdateAsync(user);
        return updatedUser;
    }
}
