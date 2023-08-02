namespace boards.Domain.Models;

public class ReplyDomain
{
    public required int Id { get; set; }
    public required string Message { get; set; }
    public DateTime? CreatedAt { get; set; }
}