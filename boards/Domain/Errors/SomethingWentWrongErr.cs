namespace boards.Domain.Errors;

public class SomethingWentWrongErr : IAppError
{
    public int Id { get; init; } = 1006;
    public string Message { get; init; } = "Something went wrong";
}