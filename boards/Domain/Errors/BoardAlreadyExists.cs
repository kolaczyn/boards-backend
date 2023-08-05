namespace boards.Domain.Errors;

public class BoardAlreadyExists : IAppError
{
    public int Id { get; init; } = 1001;
    public string Message { get; init; } = "Board already exists";
}