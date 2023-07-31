namespace boards.Domain.Errors;

public class BoardAlreadyExists : IAppError
{
    public string Message { get; set; }
    public BoardAlreadyExists()
    {
        Message = "Board already exists";
    }
}