namespace boards.Domain.Errors;

public class WrongPasswordErr : IAppError
{
    public string Message { get; set; }
    public WrongPasswordErr()
    {
        Message = "Wrong password";
    }
}