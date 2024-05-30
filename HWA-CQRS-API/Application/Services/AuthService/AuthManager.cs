using Core.Persistence.Repositories;
using Core.Security.JWT;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence.Contexts;

namespace Application.Services.AuthService;

public class AuthManager : IAuthService
{
    private readonly ITokenHelper _tokenHelper;
    private readonly TokenOptions? _tokenOptions;
    private readonly IRepository<UserRole, AppDbContext> _repository;

    public AuthManager(
        ITokenHelper tokenHelper,
        IConfiguration configuration, IRepository<UserRole, AppDbContext> repository)
    {
        _tokenHelper = tokenHelper;
        _repository = repository;
        _tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
    }

    public async Task<AccessToken> CreateAccessToken(User user)
    {

        IList<Role> roles = await _repository
            .Query()
            .AsNoTracking()
            .Where(p => p.UserId == user.Id)
            .Select(p => new Role { Id = p.RoleId, Name = p.Role.Name })
            .ToListAsync();

        var accessToken = _tokenHelper.CreateToken(user, roles ?? new List<Role>());
        return accessToken;
    }
}
