namespace boards.Domain.Errors;

public class BoardAlreadyExists : AppError
{
    public BoardAlreadyExists()
    {
        Message = "Board already exists";
    }
}