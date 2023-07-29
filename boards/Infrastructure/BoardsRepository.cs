using boards.Domain;

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
}