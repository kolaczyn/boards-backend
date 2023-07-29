namespace boards.Infrastructure.Models;

public class ReplyDb
{
    public int Id { get; set; }
    public string Message { get; set; }
    public BoardDb board { get; set; }
}