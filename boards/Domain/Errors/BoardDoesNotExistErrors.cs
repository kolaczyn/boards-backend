namespace boards.Domain.Errors;

public class BoardDoesNotExistErrors : AppError
{
    public BoardDoesNotExistErrors()
    {
        Message = "Board does not exist";
    }
}