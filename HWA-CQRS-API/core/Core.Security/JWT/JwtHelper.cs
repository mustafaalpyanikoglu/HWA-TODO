using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Core.Security.Encryption;
using Core.Security.Extensions;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Core.Security.JWT;

public class JwtHelper : ITokenHelper
{
    private IConfiguration Configuration { get; }
    private readonly TokenOptions _tokenOptions;
    private DateTime _accessTokenExpiration;

    public JwtHelper(IConfiguration configuration)
    {
        Configuration = configuration;
        _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
    }

    public AccessToken CreateToken(User user, IList<Role> roles)
    {
        _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        SecurityKey securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
        SigningCredentials signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        JwtSecurityToken jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, roles);
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
        string? token = jwtSecurityTokenHandler.WriteToken(jwt);

        return new AccessToken { Token = token, ExpirationDate = _accessTokenExpiration };
    }

    public JwtSecurityToken CreateJwtSecurityToken(
        TokenOptions tokenOptions, User user,
        SigningCredentials signingCredentials, IList<Role> roles)
    {
        JwtSecurityToken jwt =
            new(
                tokenOptions.Issuer,
                tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, roles),
                signingCredentials: signingCredentials
            );
        return jwt;
    }

    private IEnumerable<Claim> SetClaims(User user, IList<Role> roles)
    {
        List<Claim> claims = new();
        claims.AddNameIdentifier(user.Id.ToString());
        claims.AddEmail(user.Email);
        claims.AddName($"{user.Username}");
        claims.AddRoles(roles.Select(c => c.Name).ToArray());
        return claims;
    }
}