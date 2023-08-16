namespace boards.Domain.Models;

public class ReplyDomain
{
    public required int Id { get; init; }
    public required string Message { get; init; }
    public DateTime? CreatedAt { get; init; }
    public string? ImageUrl { get; init; }
    public string? Tripcode { get; init; }
}