namespace boards.Domain.Errors;

public class WrongPasswordErr : IAppError
{
    public int Id { get; init; } = 1007;
    public string Message { get; init; } = "Wrong password";
}