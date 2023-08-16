namespace boards.Infrastructure.Models;

public class ReplyDb
{
    public int Id { get; set; }
    public required string Message { get; set; }
    public required ThreadDb Thread { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? ImageUrl { get; set; }
    public string? Tripcode { get; set; }
}