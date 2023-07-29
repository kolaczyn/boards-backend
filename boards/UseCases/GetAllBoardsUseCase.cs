using boards.Dto;
using boards.Infrastructure;
using boards.Mappers;

namespace boards.UseCases;

public class GetAllBoardsUseCase
{
    public IEnumerable<BoardDto> Execute()
    {
        var result = new BoardsRepository().GetAll();
        
        return result.Select(x => x.ToDto());
    }
    
}