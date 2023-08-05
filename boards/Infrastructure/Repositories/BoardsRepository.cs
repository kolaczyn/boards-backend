using boards.Domain.Errors;
using boards.Domain.Models;
using boards.Domain.Providers;
using boards.Domain.Queries;
using boards.Domain.Repositories;
using boards.Infrastructure.Context;
using boards.Infrastructure.Mappers;
using boards.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace boards.Infrastructure.Repositories;

public class BoardsRepository : IBoardsRepository
{
    private readonly BoardDbContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;

    public BoardsRepository(BoardDbContext dbContext, IDateTimeProvider dateTimeProvider, ILogger<BoardsRepository> logger)
    {
        _dbContext = dbContext;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<IEnumerable<BoardDomain>> GetAll(CancellationToken cancellationToken)
    {
        var threads = await _dbContext.Boards.AsNoTracking()
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

    public async Task<(BoardsThreadsDomain?, IAppError?)> GetThreads(BoardThreadsQuery query,
        CancellationToken cancellationToken)
    {
        var board = await _dbContext.Boards
            .AsNoTracking()
            .Include(x => x.Threads)
            .ThenInclude(t => t.Replies)
            .Where(x => x.Slug == query.Slug)
            .FirstOrDefaultAsync(cancellationToken);

        if (board is null)
        {
            return (null, new BoardDoesNotExistError());
        }

        var sortedThreads = query.SortOrder switch
        {
            ThreadSortOrder.CreationDate => board.Threads.OrderByDescending(x => x.CreatedAt),
            ThreadSortOrder.ReplyCount => board.Threads.OrderByDescending(x => x.Replies.Count),
            ThreadSortOrder.Bump  => board.Threads.OrderByDescending(x => x.Replies.Last().CreatedAt),
        };
        
        var paginatedThreads = sortedThreads
            // not the most optimal solution - we should paginate in the sql, but this should work for now :p
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize).Select(x => new ThreadTeaserDomain
            {
                Id = x.Id,
                Message = x.Title ?? x.Replies.FirstOrDefault()?.Message ?? "",
                RepliesCount = x.Replies.Count,
                CreatedAt = x.CreatedAt,
                ImageUrl = x.Replies.FirstOrDefault()?.ImageUrl
            });

        // again - sorting should probably be done on the sql level, but whatever :D

        var result = new BoardsThreadsDomain
        {
            Name = board.Name,
            Slug = board.Slug,
            Threads = paginatedThreads
        };
        

        return (result, null);
    }

    public async Task<ThreadDomain?> CreateThread(string slug, string? title, string message, string? imageUrl, CancellationToken cancellationToken)
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
            Title = title,
            Replies = new List<ReplyDb>(),
            CreatedAt = now
        };

        _dbContext.Threads.Add(newThread);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var newReply = new ReplyDb
        {
            Message = message,
            Thread = newThread,
            CreatedAt = now,
            ImageUrl = imageUrl
        };
        _dbContext.Replies.Add(newReply);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var thread = await _dbContext.Threads.FindAsync(newThread.Id, cancellationToken);
        return new ThreadDomain
        {
            Board = thread.Board.ToDomain(),
            Id = thread.Id,
            Replies = thread.Replies.Select(x => x.ToDomain()),
            CreatedAt = now,
            Title = thread.Title,
        };
    }

    public async Task<(ReplyDomain?, IAppError?)> CreateReply(int threadId, string message,
        string? imageUrl,
        CancellationToken cancellationToken)
    {
        var thread = await _dbContext.Threads.FindAsync(threadId, cancellationToken);
        if (thread is null)
        {
            return (null, new ThreadDoesNotExist());
        }

        var now = _dateTimeProvider.Now();
        var newReply = new ReplyDb
        {
            Message = message,
            Thread = thread,
            CreatedAt = now,
            ImageUrl = imageUrl
        };
        _dbContext.Replies.Add(newReply);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var reply = await _dbContext.Replies.FindAsync(newReply.Id, cancellationToken);

        if (reply is null)
        {
            return (null, new SomethingWentWrongErr());
        }
        
        return (reply.ToDomain(), null);
    }

    public async Task<ReplyDomain?> GetReply(string slug, int threadId, int replyId, CancellationToken cancellationToken)
    {
        var reply = await _dbContext.Replies.AsNoTracking()
            .Include(x => x.Thread)
            .ThenInclude(x => x.Board)
            .FirstOrDefaultAsync(x => x.Id == replyId && x.Thread.Id == threadId && x.Thread.Board.Slug == slug,
                cancellationToken);

        return reply?.ToDomain();
    }

    public async Task<ReplyDomain?> DeleteReply(string slug, int threadId, int replyId, CancellationToken cancellationToken)
    {
        var reply = await _dbContext.Replies
            .Include(x => x.Thread)
            .ThenInclude(x => x.Board)
            .FirstOrDefaultAsync(x => x.Id == replyId && x.Thread.Id == threadId && x.Thread.Board.Slug == slug,
                cancellationToken);

        if (reply == null)
        {
            return null;
        }
        
        _dbContext.Replies.Remove(reply);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return reply.ToDomain();
    }

    public async Task<ThreadDomain?> GetThread(string slug, int threadId, CancellationToken cancellationToken)
    {
        var thread = await _dbContext.Threads.AsNoTracking()
            .Where(x => x.Board.Slug == slug)
            .Include(x => x.Replies)
            .Include(x => x.Board)
            .FirstOrDefaultAsync(x => x.Id == threadId,
                cancellationToken);

        return thread?.ToDomain();
    }
}