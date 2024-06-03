using System.Text.RegularExpressions;
using Core.CrossCuttingConcerns.Exceptions.Extensions;
using Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Microsoft.AspNetCore.Http;

namespace Core.CrossCuttingConcerns.Exceptions.Handlers;

/// <summary>
/// Exception handler class to handle and format different types of exceptions as HTTP Problem Details responses.
/// </summary>
public class HttpExceptionHandler : ExceptionHandler
{
    public HttpResponse Response
    {
        get => _response ?? throw new ArgumentNullException(nameof(_response));
        set => _response = value;
    }

    private HttpResponse? _response;

    protected override Task HandleException(BusinessException businessException)
    {
        Response.StatusCode = StatusCodes.Status400BadRequest;
        var errorLocation = ExtractFilePathAndLineNumber(businessException.StackTrace!);
        var details = new BusinessProblemDetails(businessException.Message, errorLocation).AsJson();
        return Response.WriteAsync(details);
    }

    protected override Task HandleException(ValidationException validationException)
    {
        Response.StatusCode = StatusCodes.Status400BadRequest;
        var errorLocation = ExtractFilePathAndLineNumber(validationException.StackTrace!);
        var details = new ValidationProblemDetails(validationException.Errors, errorLocation).AsJson();
        return Response.WriteAsync(details);
    }

    protected override Task HandleException(AuthorizationException authorizationException)
    {
        Response.StatusCode = StatusCodes.Status401Unauthorized;
        var errorLocation = ExtractFilePathAndLineNumber(authorizationException.StackTrace!);
        var details = new AuthorizationProblemDetails(authorizationException.Message, errorLocation).AsJson();
        return Response.WriteAsync(details);
    }

    protected override Task HandleException(NotFoundException notFoundException)
    {
        Response.StatusCode = StatusCodes.Status404NotFound;
        var errorLocation = ExtractFilePathAndLineNumber(notFoundException.StackTrace!);
        var details = new NotFoundProblemDetails(notFoundException.Message, errorLocation).AsJson();
        return Response.WriteAsync(details);
    }

    protected override Task HandleException(Exception exception)
    {
        Response.StatusCode = StatusCodes.Status500InternalServerError;
        var errorLocation = ExtractFilePathAndLineNumber(exception.StackTrace!);
        var details = new InternalServerErrorProblemDetails(exception.Message, errorLocation).AsJson();
        return Response.WriteAsync(details);
    }
    
    /// Returns a string containing the file path and line number, or an empty string if not found.
    private string ExtractFilePathAndLineNumber(string stackTrace)
    {
        if (string.IsNullOrEmpty(stackTrace))
        {
            return string.Empty;
        }
        
        var regex = new Regex(@" in (.*):line (\d+)");
        
        var match = regex.Match(stackTrace);
        return match.Success ? $"{match.Groups[1].Value}:line {match.Groups[2].Value}" : string.Empty;
    }
}
