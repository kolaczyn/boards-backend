namespace boards.Domain.Errors;

public class BoardDoesNotExistError : IAppError
{
    public int Id { get; init; } = 1002;
    public string Message { get; init; } = "Board does not exist";

}