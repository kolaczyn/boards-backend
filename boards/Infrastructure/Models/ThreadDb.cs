namespace boards.Infrastructure.Models;

public class ThreadDb
{
    public int Id { get; set; }
    public required List<ReplyDb> Replies { get; set; }
    public required BoardDb Board { get; set; }
}