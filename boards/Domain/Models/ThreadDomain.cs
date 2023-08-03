namespace boards.Domain.Models;

public sealed class ThreadDomain
{
    public required int Id { get; init; }
    public string? Title { get; init; }
    public required IEnumerable<ReplyDomain> Replies { get; init; }
    public required BoardDomain Board { get; set; }
    public DateTime? CreatedAt { get; init; }
}