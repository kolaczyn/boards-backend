namespace boards.Domain.Errors;

public class ReplyDoesNotExist : IAppError
{
    public int Id { get; init; } = 1004;
    public string Message { get; init; } = "Reply does not exist";
}