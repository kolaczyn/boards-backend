
using boards.Application.Dto;

namespace boards.Domain;

public interface IBoardsRepository
{
    public IEnumerable<BoardDomain> GetAll();
    public BoardDomain? GetBySlug(string slug);
    public BoardDomain Create(BoardDomain board);
    public IEnumerable<ThreadDomain> GetThreadsBySlug(string slug);
}