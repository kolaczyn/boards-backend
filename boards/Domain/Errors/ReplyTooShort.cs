namespace boards.Domain.Errors;

public class ReplyTooShort : IAppError
{
    public string Message { get; set; }
}