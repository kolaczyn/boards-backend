using FluentResults;

namespace boards.Domain;

public interface IBoardsRepository
{
    public Task<IEnumerable<BoardDomain>> GetAll(CancellationToken cancellationToken);
    public Task<Result<BoardDomain>> GetBySlug(string slug, CancellationToken cancellationToken);
    public Task<BoardDomain?> Create(BoardDomain board, CancellationToken cancellationToken);
    public Task<IEnumerable<ThreadDomain>> GetThreadsBySlug(string slug, CancellationToken cancellationToken);
    public Task<ThreadDomain?> CreateThread(string slug, string message, CancellationToken cancellationToken);
    public Task<ReplyDomain?> CreateReply(string slug, int threadId, string message,
        CancellationToken cancellationToken);
    public Task<ThreadDomain?> GetThread(string slug, int threadId, CancellationToken cancellationToken);
}