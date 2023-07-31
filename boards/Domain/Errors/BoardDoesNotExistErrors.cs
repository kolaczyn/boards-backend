namespace boards.Domain.Errors;

public class BoardDoesNotExistErrors : IAppError
{
    public string Message { get; set; }
    public BoardDoesNotExistErrors()
    {
        Message = "Board does not exist";
    }

}