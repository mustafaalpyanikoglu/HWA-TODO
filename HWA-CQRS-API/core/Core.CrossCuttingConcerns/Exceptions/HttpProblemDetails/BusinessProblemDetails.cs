using Core.CrossCuttingConcerns.Exceptions.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;

public class BusinessProblemDetails : ProblemDetails
{
    public string ErrorLocation { get; }
    public BusinessProblemDetails(string details, string errorLocation,  string instance = "")
    {
        Title = Messages.BusinessErrorTitle;
        Detail = details;
        ErrorLocation = errorLocation;
        Status = StatusCodes.Status400BadRequest;
        Type = Messages.BusinessErrorType;
        Instance = instance;
    }
}