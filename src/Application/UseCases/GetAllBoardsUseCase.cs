using boards.Application.Mappers;
using boards.Domain;
using boards.Dto;

namespace boards.Application;

public class GetAllBoardsUseCase
{
    private readonly IBoardsRepository _repository;
    public GetAllBoardsUseCase(IBoardsRepository repository)
    {
        _repository = repository;
    }
    public IEnumerable<BoardDto> Execute()
    {
        var result = _repository.GetAll();
        
        return result.Select(x => x.ToDto());
    }
    
}