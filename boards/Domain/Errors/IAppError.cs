namespace boards.Domain.Errors;

public interface IAppError
{
    public string Message { get; set; }
}