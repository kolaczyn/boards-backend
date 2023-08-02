using boards.Application.Mappers;
using boards.Domain.Repositories;
using boards.Dto;

namespace boards.Application.UseCases;

public class GetAllBoardsUseCase
{
    private readonly IBoardsRepository _repository;
    public GetAllBoardsUseCase(IBoardsRepository repository)
    {
        _repository = repository;
    }
    public async Task<IEnumerable<BoardDto>> Execute(CancellationToken cancellationToken)
    {
        var result = await _repository.GetAll(cancellationToken);
        
        return result.Select(x => x.ToDto());
    }
    
}