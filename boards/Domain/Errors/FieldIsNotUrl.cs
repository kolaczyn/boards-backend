namespace boards.Domain.Errors;

public class FieldIsNotUrl : IAppError
{
    public int Id { get; init; } = 1003;
    public string Message { get; init; } = "The field is not a url";
}