using System.Net.Mime;
using Core.CrossCuttingConcerns.Exceptions.Handlers;
using Microsoft.AspNetCore.Http;

namespace Core.CrossCuttingConcerns.Exceptions.Middleware;

public class ExceptionMiddleware
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly HttpExceptionHandler _httpExceptionHandler;
    private readonly RequestDelegate _next;
    
    public ExceptionMiddleware(RequestDelegate next, IHttpContextAccessor contextAccessor)
    {
        _next = next;
        _contextAccessor = contextAccessor;
        _httpExceptionHandler = new HttpExceptionHandler();
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context.Response, exception);
        }
    }

    /// <returns>
    /// It processes and corrects mistakes.
    /// </returns>
    private Task HandleExceptionAsync(HttpResponse response, Exception exception)
    {
        response.ContentType = MediaTypeNames.Application.Json;
        _httpExceptionHandler.Response = response;
        return _httpExceptionHandler.HandleExceptionAsync(exception);
    }
}