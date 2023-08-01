namespace boards.Domain;

public class ThreadTeaserDomain
{
    public required int Id { get; set; }
    public required string Message { get; set; }
    public required int RepliesCount { get; set; }
    public required DateTime? CreatedAt { get; set; }
}