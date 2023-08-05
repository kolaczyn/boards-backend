namespace boards.Domain.Errors;

public class ReplyTooShort : IAppError
{
    public int Id { get; init; } = 1005;
    public string Message { get; init; } = "Reply too short";
}