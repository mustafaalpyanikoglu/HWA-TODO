using Application.Constants;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Persistence.Repositories;
using Core.Security.Hashing;
using Domain.Models;
using Persistence.Contexts;

namespace Application.Rules;

public class AuthBusinessRules(IRepository<User, AppDbContext> repository) 
    : BaseBusinessRules
{
    private readonly IRepository<User, AppDbContext> _repository = repository;
    
    public Task UserShouldBeExists(User? user)
    {
        if (user == null)
            throw new NotFoundException(AuthMessages.UserDontExists);
        return Task.CompletedTask;
    }
    
    public async Task UserEmailShouldBeNotExists(string email)
    {
        var user = await _repository.GetFirstOrDefaultAsync(predicate: u => u.Email == email, enableTracking: false);
        if (user != null)
            throw new BusinessException(AuthMessages.UserMailAlreadyExists);
    }

    public async Task UserPasswordShouldBeMatch(int id, string password)
    {
        var user = await _repository.GetFirstOrDefaultAsync(predicate: u => u.Id == id, enableTracking: false);
        if (!HashingHelper.VerifyPasswordHash(password, user!.PasswordHash, user.PasswordSalt))
            throw new BusinessException(AuthMessages.PasswordDontMatch);
    }
}
