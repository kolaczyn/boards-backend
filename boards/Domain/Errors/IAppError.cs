namespace boards.Domain.Errors;

public interface IAppError
{
    public int Id { get; init; }
    public string Message { get; init; }
}