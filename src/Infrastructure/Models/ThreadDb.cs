namespace boards.Infrastructure.Models;

public class ThreadDb
{
    public int Id { get; set; }
    public List<ReplyDb> Replies { get; set; }
    public BoardDb Board { get; set; }
}