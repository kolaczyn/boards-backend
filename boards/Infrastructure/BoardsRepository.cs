using boards.Domain;
using boards.Infrastructure.Mappers;
using boards.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace boards.Infrastructure;

public class BoardsRepository : IBoardsRepository
{
    private readonly BoardDbContext _dbContext;

    public BoardsRepository(BoardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<BoardDomain>> GetAll()
    {
        var threads = await _dbContext.Boards
            .ToListAsync();
        
        return threads.Select(x => x.ToDomain());
    }

    public async Task<BoardDomain?> GetBySlug(string slug)
    {
        return (await _dbContext.Boards.FindAsync(slug))?.ToDomain();
    }

    public async Task<BoardDomain?> Create(BoardDomain board)
    {
        _dbContext.Add(board.ToDb());
        await _dbContext.SaveChangesAsync();

        return board;
    }

    public async Task<IEnumerable<ThreadDomain>> GetThreadsBySlug(string slug)
    {
        var threads =await _dbContext.Threads
            .Include(x => x.Replies)
            .Include(x => x.Board)
            .Where(x => x.Board.Slug == slug)
            .ToListAsync();
        
        return threads.Select(x => x.ToDomain());
    }

    public async Task<ThreadDomain?> CreateThread(string slug, string message)
    {
        var board = await _dbContext.Boards.FindAsync(slug);
        if (board == null)
        {
            return null;
        }

        var newThread = new ThreadDb
        {
            Board = board,
            Replies = new List<ReplyDb>(),
        };

        _dbContext.Threads.Add(newThread);
        await _dbContext.SaveChangesAsync();

        var newReply = new ReplyDb
        {
            Message = message,
            Thread = newThread
        };
        _dbContext.Replies.Add(newReply);
        await _dbContext.SaveChangesAsync();

        var thread = await _dbContext.Threads.FindAsync(newThread.Id);
        return thread?.ToDomain();
    }

    public async Task<ReplyDomain?> CreateReply(string slug, int threadId, string message)
    {
        var thread = await _dbContext.Threads.FindAsync(threadId);
        if (thread == null)
        {
            return null;
        }
        
        var newReply = new ReplyDb
        {
            Message = message,
            Thread = thread
        };
        _dbContext.Replies.Add(newReply);
        await _dbContext.SaveChangesAsync();
        
        var reply = await _dbContext.Replies.FindAsync(newReply.Id);
        return reply?.ToDomain();

    }

    public async Task<ThreadDomain?> GetThread(string slug, int threadId)
    {
        var thread = await _dbContext.Threads.Where(x => x.Board.Slug == slug)
            .Include(x => x.Replies)
            .Include(x => x.Board)
            .FirstOrDefaultAsync(x => x.Id == threadId);

        return thread?.ToDomain();
    }
}