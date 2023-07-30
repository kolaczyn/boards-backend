
namespace boards.Domain;

public interface IBoardsRepository
{
    public Task<IEnumerable<BoardDomain>> GetAll();
    public Task<BoardDomain?> GetBySlug(string slug);
    public Task<BoardDomain?> Create(BoardDomain board);
    public Task<IEnumerable<ThreadDomain>> GetThreadsBySlug(string slug);
    public Task<ThreadDomain?> CreateThread(string slug, string message);
    public Task<ReplyDomain?> CreateReply(string slug, int threadId, string message);
    public Task<ThreadDomain?> GetThread(string slug, int threadId);
}