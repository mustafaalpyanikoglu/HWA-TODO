using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Register;
using Core.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
    {
        LoginCommand loginCommand = new() { UserForLoginDto = userForLoginDto };
        var result = await _mediator.Send(loginCommand);
        return Ok(result.ToHttpResponse());
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> Register([FromBody]UserForRegisterDto userForRegisterDto)
    {
        RegisterCommand registerCommand = new() { UserForRegisterDto = userForRegisterDto };
        var result = await _mediator.Send(registerCommand);
        return Created(uri: "", result.AccessToken);
    }
}
