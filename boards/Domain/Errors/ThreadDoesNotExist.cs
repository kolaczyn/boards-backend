namespace boards.Domain.Errors;

public class ThreadDoesNotExist : IAppError
{
    public int Id { get; init; } = 1008;
    public string Message { get; init; } = "Thread does not exist";
}