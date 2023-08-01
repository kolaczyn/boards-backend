namespace boards.Domain.Errors;

public class SomethingWentWrongErr : IAppError
{
    public SomethingWentWrongErr()
    {
        Message = "Something went wrong";
    }
    public string Message { get; set; }
}