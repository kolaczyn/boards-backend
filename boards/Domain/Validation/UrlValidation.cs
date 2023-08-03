using boards.Domain.Errors;

namespace boards.Domain.Validation;

public static class UrlValidation
{
    public static IAppError? Validate(string? url)
    {
        if (url is null)
        {
            return new FieldIsNotUrl();
        }
        
        if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
        {
            return new FieldIsNotUrl();
        }

        return null;
    }
    
}