namespace boards.Domain.Errors;

public class FieldIsNotUrl :IAppError
{
    public string Message { get; set; }

    public FieldIsNotUrl()
    {
        Message = "The field is not a url";
    }
}