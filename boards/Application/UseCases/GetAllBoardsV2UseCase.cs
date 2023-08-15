using boards.Application.Dto;
using boards.Application.Mappers;
using boards.Domain.Repositories;

namespace boards.Application.UseCases;

public class GetAllBoardsV2UseCase
{
    private readonly IBoardsRepository _repository;
    public GetAllBoardsV2UseCase(IBoardsRepository repository)
    {
        _repository = repository;
    }
        
    public async Task<IEnumerable<CategoriesBoardsDto>> Execute(CancellationToken cancellationToken) =>
        (await _repository.GetCategoriesBoards(cancellationToken)).Select(x => x.ToDto());
}