namespace boards.Domain;

public class ReplyDomain
{
    public required string Message { get; set; }
    public required DateTime? CreatedAt { get; set; }
}