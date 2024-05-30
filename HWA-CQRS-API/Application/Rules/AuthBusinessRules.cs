using Application.Constants;
using Core.Application.Rules;
using Core.Persistence.Repositories;
using Domain.Models;
using Persistence.Contexts;

namespace Application.Rules;

public class AuthBusinessRules(IRepository<User, AppDbContext> repository) : BaseBusinessRules
{
    private readonly IRepository<User, AppDbContext> _repository = repository;
    
    public Task UserShouldBeExists(User? user)
    {
        if (user == null)
            throw new Exception(AuthMessages.UserDontExists);
        return Task.CompletedTask;
    }
    
    public async Task UserEmailShouldBeNotExists(string email)
    {
        User? user = await _repository.GetSingleOrDefaultAsync(predicate: u => u.Email == email, enableTracking: false);
        if (user != null)
            throw new Exception(AuthMessages.UserMailAlreadyExists);
    }

    public async Task UserPasswordShouldBeMatch(int id, string password)
    {
        User? user = await _repository.GetSingleOrDefaultAsync(predicate: u => u.Id == id, enableTracking: false);
        if (user == null || user.Password != password)
            throw new Exception(AuthMessages.PasswordDontMatch);
    }
}
