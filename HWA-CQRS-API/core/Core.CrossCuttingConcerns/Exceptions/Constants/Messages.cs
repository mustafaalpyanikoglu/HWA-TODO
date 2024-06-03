namespace Core.CrossCuttingConcerns.Exceptions.Constants;

public static class Messages
{
    public const string AuthorizationErrorTitle = "Authorization Error";
    public const string AuthorizationErrorType = "https://example.com/probs/authorization";
    
    public const string BusinessErrorTitle = "Rule Validation";
    public const string BusinessErrorType = "https://example.com/probs/business";
    
    public const string InternalServerErrorTitle = "Internal Server Error";
    public const string InternalServerErrorType = "https://example.com/probs/internal";
    
    public const string NotFoundErrorTitle = "Not Found";
    public const string NotFoundErrorType = "https://example.com/probs/notfound";
    
    public const string ValidationErrorTitle = "Validation error(s)";
    public const string ValidationErrorType = "https://example.com/probs/validation";

}