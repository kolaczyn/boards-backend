
namespace boards.Domain;

public interface IBoardsRepository
{
    public IEnumerable<BoardDomain> GetAll();
}