using Core.CrossCuttingConcerns.Exceptions.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;

public class ValidationProblemDetails : ProblemDetails
{
    public string ErrorLocation { get; }
    public object Failures { get; init; }

    public ValidationProblemDetails(
        object failures, string errorLocation, string instance = "", string detail = "")
    {
        Title = Messages.ValidationErrorTitle;
        Failures = failures;
        ErrorLocation = errorLocation;
        Status = StatusCodes.Status400BadRequest;
        Type = Messages.ValidationErrorType;
        Detail = detail;
        Instance = instance;
    }
}    

