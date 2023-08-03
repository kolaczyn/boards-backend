using boards.Domain.Errors;

namespace boards.Domain.Validation;

public static class ReplyValidation
{
    public static IAppError? ValidateReply(string? reply)
    {
        if (string.IsNullOrWhiteSpace(reply))
        {
            return new ReplyTooShort
            {
                Message = "Reply should be at least 1 character long"
            };
        }
        return null;
    }
}