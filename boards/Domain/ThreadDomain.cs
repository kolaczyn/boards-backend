namespace boards.Domain;

public class ThreadDomain
{
    public required int Id { get; set; }
    public required IEnumerable<ReplyDomain> Replies { get; set; }
    public required BoardDomain Board { get; set; }
    public required DateTime? CreatedAt { get; set; }
}