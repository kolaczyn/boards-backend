using boards.Domain;
using boards.Infrastructure.Mappers;
using boards.Infrastructure.Models;

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
        var result = _dbContext.Threads.AsEnumerable().Where(x => x.Board.Slug == slug);
        return result.Select(x => x.ToDomain());
    }

}