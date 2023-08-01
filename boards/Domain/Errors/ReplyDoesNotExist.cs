namespace boards.Domain.Errors;

public class ReplyDoesNotExist : IAppError
{
    public string Message { get; set; }
    public ReplyDoesNotExist()
    {
        Message = "Reply does not exist";
    }
}