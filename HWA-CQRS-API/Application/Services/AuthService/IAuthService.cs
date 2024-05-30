using Core.Security.JWT;
using Domain.Models;

namespace Application.Services.AuthService;

public interface IAuthService
{
    public Task<AccessToken> CreateAccessToken(User user);
}
