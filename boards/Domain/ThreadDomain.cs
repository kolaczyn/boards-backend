namespace boards.Domain;

public class ThreadDomain
{
    public int Id { get; set; }
    public IEnumerable<ReplyDomain> Replies { get; set; }
    public BoardDomain Board { get; set; }
}