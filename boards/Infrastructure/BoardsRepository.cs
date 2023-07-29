using boards.Dto;

namespace boards.Infrastructure;

public class BoardsRepository
{
    public IEnumerable<BoardDb> GetAll()
    {
        var boards = new List<BoardDb>
        {
            new() { Slug = "a", Name = "anime" },
            new() { Slug = "b", Name = "random" }
        };

        return boards;
    }
}