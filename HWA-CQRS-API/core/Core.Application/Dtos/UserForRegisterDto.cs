using System.Text.Json.Serialization;

namespace Core.Application.Dtos;

public class UserForRegisterDto : IDto
{
    public required string Username { get; set; }
    public required string Email { get; set; }

    [JsonIgnore]
    public string Password { get; set; }

    public UserForRegisterDto()
    {
        Email = string.Empty;
        Username = string.Empty;
        Password = string.Empty;
    }

    public UserForRegisterDto(string username, string email, string password)
    {
        Username = username;
        Email = email;
        Password = password;
    }
}
