using Core.CrossCuttingConcerns.Exceptions.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;

public class AuthorizationProblemDetails : ProblemDetails
{
    public string ErrorLocation { get; }
    public AuthorizationProblemDetails(string detail, string errorLocation, string instance = "")
    {
        Title = Messages.AuthorizationErrorTitle;
        Detail = detail;
        ErrorLocation = errorLocation;
        Status = StatusCodes.Status401Unauthorized;
        Type = Messages.AuthorizationErrorType;
        Instance = instance;
    }
}