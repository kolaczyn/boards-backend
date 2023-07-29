
namespace boards.Domain;

public interface IBoardsRepository
{
    public IEnumerable<BoardDomain> GetAll();
    public BoardDomain? GetBySlug(string slug);
    public BoardDomain Create(BoardDomain board);
}