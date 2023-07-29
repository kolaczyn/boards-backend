using boards.Domain;
using boards.Dto;

namespace boards.Application;

public class GetBoardBySlugUseCase
{
    private readonly IBoardsRepository _repository;

    public GetBoardBySlugUseCase(IBoardsRepository repository)
    {
        _repository = repository;
    }

    public BoardDto? Execute(string slug)
    {
        var result = _repository.GetBySlug(slug);
        return result?.ToDto();
    }
}