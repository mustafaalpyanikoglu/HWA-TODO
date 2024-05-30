using Application.Rules;
using Application.Services.AuthService;
using Core.Application.Dtos;
using Core.Persistence.Repositories;
using Core.Security.Hashing;
using Domain.Models;
using MediatR;
using Persistence.Contexts;

namespace Application.Features.Auth.Commands.Register;

public class RegisterCommand : IRequest<RegisteredResponse>
{
    public UserForRegisterDto UserForRegisterDto { get; set; }

    public class RegisterCommandHandler(IRepository<User, AppDbContext> repository, IAuthService authService, AuthBusinessRules authBusinessRules)
        : IRequestHandler<RegisterCommand, RegisteredResponse>
    {
        private readonly IRepository<User, AppDbContext> _repository = repository;
        private readonly IAuthService _authService = authService;
        private readonly AuthBusinessRules _authBusinessRules = authBusinessRules;

        public async Task<RegisteredResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await _authBusinessRules.UserEmailShouldBeNotExists(request.UserForRegisterDto.Email);
            
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out passwordHash, out passwordSalt);

            User newUser = new()
            {
                Email = request.UserForRegisterDto.Email,
                Username = request.UserForRegisterDto.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                CreatedDate = DateTime.UtcNow,
            };
            var createdUser = await _repository.AddAsync(newUser);

            var createdAccessToken = await _authService.CreateAccessToken(createdUser);

            var registeredDto = new RegisteredResponse()
                { AccessToken = createdAccessToken };
            return registeredDto;
        }
    }
}