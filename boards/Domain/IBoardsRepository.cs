
namespace boards.Domain;

public interface IBoardsRepository
{
    public IEnumerable<BoardDomain> GetAll();
    public BoardDomain? GetBySlug(string slug);
    public BoardDomain Create(BoardDomain board);
    public IEnumerable<ThreadDomain> GetThreadsBySlug(string slug);
    public ThreadDomain? CreateThread(string slug, string message);
    public ReplyDomain? CreateReply(string slug, int threadId, string message);
    public ThreadDomain? GetThread(string slug, int threadId);
}