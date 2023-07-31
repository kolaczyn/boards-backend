using boards.Domain;
using boards.Infrastructure.Mappers;
using boards.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace boards.Infrastructure;

public class BoardsRepository : IBoardsRepository
{
    private readonly BoardDbContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;

    public BoardsRepository(BoardDbContext dbContext, IDateTimeProvider dateTimeProvider)
    {
        _dbContext = dbContext;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<IEnumerable<BoardDomain>> GetAll(CancellationToken cancellationToken)
    {
        var threads = await _dbContext.Boards
            .ToListAsync(cancellationToken);

        return threads.Select(x => x.ToDomain());
    }

    public async Task<BoardDomain?> GetBySlug(string slug, CancellationToken cancellationToken)
    {
        return (await _dbContext.Boards.FindAsync(slug, cancellationToken))?.ToDomain();
    }

    public async Task<BoardDomain?> Create(BoardDomain board, CancellationToken cancellationToken)
    {
        _dbContext.Add(board.ToDb());
        await _dbContext.SaveChangesAsync(cancellationToken);

        return board;
    }

    public async Task<BoardsThreadsDomain?> GetThreadsBySlug(string slug, CancellationToken cancellationToken)
    {
        var board = await _dbContext.Boards
            .Include(x => x.Threads)
            .ThenInclude(t => t.Replies)
            .Where(x => x.Slug == slug)
            .FirstOrDefaultAsync(cancellationToken);

        if (board is null)
        {
            return null;
        }

        return new BoardsThreadsDomain
        {
            Name = board.Name,
            Slug = board.Slug,
            Threads = board.Threads.Select(x => new ThreadTeaserDomain()
            {
                Id = x.Id,
                Message = x.Replies.FirstOrDefault()?.Message ?? "",
                RepliesCount = x.Replies.Count,
                CreatedAt = x.CreatedAt
            })
        };

    }

    public async Task<ThreadDomain?> CreateThread(string slug, string message, CancellationToken cancellationToken)
    {
        var board = await _dbContext.Boards.FindAsync(slug, cancellationToken);
        if (board == null)
        {
            return null;
        }

        var now = _dateTimeProvider.Now();
        var newThread = new ThreadDb
        {
            Board = board,
            Replies = new List<ReplyDb>(),
            CreatedAt = now,
        };

        _dbContext.Threads.Add(newThread);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var newReply = new ReplyDb
        {
            Message = message,
            Thread = newThread,
            CreatedAt = now,
        };
        _dbContext.Replies.Add(newReply);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var thread = await _dbContext.Threads.FindAsync(newThread.Id);
        return new ThreadDomain
        {
            Board = thread.Board.ToDomain(),
            Id = thread.Id,
          Replies = thread.Replies.Select(x => x.ToDomain()),
          CreatedAt = now
        };
    }

    public async Task<ReplyDomain?> CreateReply(string slug, int threadId, string message,
        CancellationToken cancellationToken)
    {
        var thread = await _dbContext.Threads.FindAsync(threadId, cancellationToken);
        if (thread == null)
        {
            return null;
        }

        var now = _dateTimeProvider.Now();
        var newReply = new ReplyDb
        {
            Message = message,
            Thread = thread,
            CreatedAt = now
        };
        _dbContext.Replies.Add(newReply);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var reply = await _dbContext.Replies.FindAsync(newReply.Id, cancellationToken);
        return reply?.ToDomain();
    }

    public async Task<ThreadDomain?> GetThread(string slug, int threadId, CancellationToken cancellationToken)
    {
        var thread = await _dbContext.Threads.Where(x => x.Board.Slug == slug)
            .Include(x => x.Replies)
            .Include(x => x.Board)
            .FirstOrDefaultAsync(x => x.Id == threadId, cancellationToken);

        return thread?.ToDomain();
    }
}