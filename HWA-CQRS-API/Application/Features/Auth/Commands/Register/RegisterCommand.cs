using Application.Rules;
using Application.Services.AuthService;
using Core.Application.Dtos;
using Core.Persistence.Repositories;
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

            User newUser = new()
            {
                Email = request.UserForRegisterDto.Email,
                Username = request.UserForRegisterDto.Username,
                Password = request.UserForRegisterDto.Password,
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