using boards.Domain;

namespace boards.Infrastructure;

public class BoardsRepository : IBoardsRepository
{
    private IEnumerable<BoardDb> Db()
    {
        var db = new List<BoardDb>
        {
            new() { Slug = "a", Name = "anime" },
            new() { Slug = "b", Name = "random" }
        };
        return db;
    }
    public IEnumerable<BoardDomain> GetAll()
    {
        var db = Db();
        var domain = db.Select(x => x.ToDomain());
        return domain;
    }

    public BoardDomain? GetBySlug(string slug)
    {
        var db = Db();
        var domain = db.FirstOrDefault(x => x.Slug == slug)?.ToDomain();
        return domain;
    }
}