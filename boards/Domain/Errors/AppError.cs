namespace boards.Domain.Errors;

/// <summary>
/// This should be only be used for Swagger in docs
/// </summary>
public class AppError : IAppError
{
    public int Id { get; init; }
    public string Message { get; init; } = "";
}