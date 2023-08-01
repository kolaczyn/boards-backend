namespace boards.Domain.Errors;

public class BoardDoesNotExistError : IAppError
{
    public string Message { get; set; }
    public BoardDoesNotExistError()
    {
        Message = "Board does not exist";
    }

}