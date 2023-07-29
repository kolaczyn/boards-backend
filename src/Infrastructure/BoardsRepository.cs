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

    public IEnumerable<BoardDomain> GetAll()
    {
        var db = _dbContext.Set<BoardDb>();
        var domain = db.Select(x => x.ToDomain());
        return domain;
    }

    public BoardDomain? GetBySlug(string slug)
    {
        return _dbContext.Boards.Find(slug)?.ToDomain();
    }

    public BoardDomain Create(BoardDomain board)
    {
        _dbContext.Add(board.ToDb());
        _dbContext.SaveChanges();

        return board;
    }

    public IEnumerable<ThreadDomain> GetThreadsBySlug(string slug)
    {
        var threads = _dbContext.Threads
            .Include(x => x.Replies)
            .Include(x => x.Board)
            .Where(x => x.Board.Slug == slug)
            .ToList();
        
        return threads.Select(x => x.ToDomain());
    }

    public ThreadDomain? CreateThread(string slug, string message)
    {
        var board = _dbContext.Boards.Find(slug);
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
        _dbContext.SaveChanges();

        var newReply = new ReplyDb
        {
            Message = message,
            Thread = newThread
        };
        _dbContext.Replies.Add(newReply);
        _dbContext.SaveChanges();

        var thread = _dbContext.Threads.Find(newThread.Id);
        return thread?.ToDomain();
    }
}