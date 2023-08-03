using boards.Domain.Errors;
using boards.Domain.Models;
using boards.Domain.Queries;

namespace boards.Domain.Repositories;

public interface IBoardsRepository
{
    public Task<BoardDomain?> Create(BoardDomain board, CancellationToken cancellationToken);
    public Task<BoardDomain?> GetBySlug(string slug, CancellationToken cancellationToken);
    public Task<IEnumerable<BoardDomain>> GetAll(CancellationToken cancellationToken);

    public Task<(BoardsThreadsDomain?, IAppError?)> GetThreads(BoardThreadsQuery query, CancellationToken cancellationToken);

    public Task<ThreadDomain?> CreateThread(string slug, string? title, string message, CancellationToken cancellationToken);
    public Task<ThreadDomain?> GetThread(string slug, int threadId, CancellationToken cancellationToken);
    
    public Task<ReplyDomain?> CreateReply(string slug, int threadId, string message,
        CancellationToken cancellationToken);
    public Task<ReplyDomain?> GetReply(string slug, int threadId, int replyId, CancellationToken cancellationToken);
    public Task<ReplyDomain?> DeleteReply(string slug, int threadId, int replyId, CancellationToken cancellationToken);
}