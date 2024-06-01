using Core.CrossCuttingConcerns.Exceptions.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;

public class InternalServerErrorProblemDetails : ProblemDetails
{
    public string ErrorLocation { get; }
    public InternalServerErrorProblemDetails(string details, string errorLocation, string instance = "")
    {
        Title = Messages.InternalServerErrorTitle;
        Detail = details;
        ErrorLocation = errorLocation;
        Status = StatusCodes.Status500InternalServerError;
        Type = Messages.InternalServerErrorType;
        Instance = instance;
    }
}