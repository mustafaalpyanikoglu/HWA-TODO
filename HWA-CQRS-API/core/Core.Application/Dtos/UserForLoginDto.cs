﻿using System.Text.Json.Serialization;

namespace Core.Application.Dtos;

public class UserForLoginDto : IDto
{
    public required string Email { get; set; }

    [JsonIgnore]
    public string Password { get; set; }

    public UserForLoginDto()
    {
        Email = string.Empty;
        Password = string.Empty;
    }

    public UserForLoginDto(string email, string password)
    {
        Email = email;
        Password = password;
    }
}
