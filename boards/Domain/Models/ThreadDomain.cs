namespace boards.Domain.Models;

public class ThreadDomain
{
    public required int Id { get; set; }
    public required IEnumerable<ReplyDomain> Replies { get; set; }
    public required BoardDomain Board { get; set; }
    public DateTime? CreatedAt { get; set; }
}