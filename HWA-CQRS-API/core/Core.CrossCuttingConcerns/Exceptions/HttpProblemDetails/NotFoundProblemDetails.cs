using Core.CrossCuttingConcerns.Exceptions.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;

public class NotFoundProblemDetails : ProblemDetails
{
    public string ErrorLocation { get; }
    public NotFoundProblemDetails(string details, string errorLocation, string instance = "")
    {
        Title = Messages.NotFoundErrorTitle;
        Detail = details;
        ErrorLocation = errorLocation;
        Status = StatusCodes.Status404NotFound;
        Type = Messages.NotFoundErrorType;
        Instance = instance;
    }
}