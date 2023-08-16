namespace boards.Domain.Errors;

public class InvalidTripcode : IAppError
{
    public int Id { get; init; } = 2000;
    public string Message { get; init; } = "Invalid tripcode";
}