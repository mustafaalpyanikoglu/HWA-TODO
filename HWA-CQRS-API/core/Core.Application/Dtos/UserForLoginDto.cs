using System.Text.Json.Serialization;

namespace Core.Application.Dtos;

public class UserForLoginDto : IDto
{
    public required string Email { get; set; }
    public string Password { get; set; }

    public UserForLoginDto(string email, string password)
    {
        Email = email;
        Password = password;
    }
}
