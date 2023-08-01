namespace boards.Domain;

public class ReplyDomain
{
    public required int Id { get; set; }
    public required string Message { get; set; }
    public required DateTime? CreatedAt { get; set; }
}