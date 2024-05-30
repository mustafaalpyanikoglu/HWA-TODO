using Core.Security.Constants;
using Core.Security.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace Core.Application.Pipelines.Authorization;

public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ISecuredRequest
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorizationBehavior(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var userRoleClaims = _httpContextAccessor.HttpContext.User.GetRoleClaims();

        if (userRoleClaims == null)
            throw new Exception("You are not authenticated.");

        var isNotMatchedAUserRoleClaimWithRequestRoles = userRoleClaims
            .FirstOrDefault(
                userRoleClaim => userRoleClaim == GeneralRoles.Admin || request.Roles.Any(role => role == userRoleClaim)
            )
            .IsNullOrEmpty();
        if (isNotMatchedAUserRoleClaimWithRequestRoles)
            throw new Exception("You are not authorized.");

        var response = await next();
        return response;
    }
}