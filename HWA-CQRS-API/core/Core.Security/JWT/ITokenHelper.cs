using Domain.Models;

namespace Core.Security.JWT;

public interface ITokenHelper
{
    public AccessToken CreateToken(User user, IList<Role> roles);
}
