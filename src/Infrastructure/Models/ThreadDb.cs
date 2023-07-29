namespace boards.Infrastructure.Models;

public class ThreadDb
{
    public int Id { get; set; }
    public ICollection<ReplyDb> Replies { get; set; }
    public BoardDb Board { get; set; }
}