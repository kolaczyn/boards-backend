using boards.Domain;

namespace boards.Infrastructure;

public class BoardsRepository : IBoardsRepository
{
    public IEnumerable<BoardDomain> GetAll()
    {
        var db = new List<BoardDb>
        {
            new() { Slug = "a", Name = "anime" },
            new() { Slug = "b", Name = "random" }
        };

        var domain = db.Select(x => x.ToDomain());
        return domain;
    }
}