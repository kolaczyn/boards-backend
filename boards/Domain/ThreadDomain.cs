namespace boards.Domain;

public class ThreadDomain
{
    public IEnumerable<ReplyDomain> Replies { get; set; }
    public BoardDomain Board { get; set; }
}