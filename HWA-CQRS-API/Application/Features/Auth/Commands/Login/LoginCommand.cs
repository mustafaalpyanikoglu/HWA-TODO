using Application.Rules;
using Application.Services.AuthService;
using Application.Services.UserService;
using Core.Application.Dtos;
using MediatR;

namespace Application.Features.Auth.Commands.Login;

public class LoginCommand : IRequest<LoggedResponse>
{
    public UserForLoginDto UserForLoginDto { get; set; }

    public class LoginCommandHandler(IUserService userService, IAuthService authService, AuthBusinessRules authBusinessRules)
        : IRequestHandler<LoginCommand, LoggedResponse>
    {
        private readonly AuthBusinessRules _authBusinessRules = authBusinessRules;
        private readonly IAuthService _authService = authService;
        private readonly IUserService _userService = userService;

        public async Task<LoggedResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetByEmail(request.UserForLoginDto.Email);
            await _authBusinessRules.UserShouldBeExists(user);
            await _authBusinessRules.UserPasswordShouldBeMatch(user!.Id, request.UserForLoginDto.Password);

            LoggedResponse loggedResponse = new();

            var createdAccessToken = await _authService.CreateAccessToken(user);

            loggedResponse.AccessToken = createdAccessToken;
            return loggedResponse;
        }
    }
}
