using boards.Domain;
using boards.Dto;

namespace boards.Application;

public class CreateBoardUseCase
{
    private readonly IBoardsRepository _repository;
    public CreateBoardUseCase(IBoardsRepository repository)
    {
        _repository = repository;
    }
    
    // this should be CreateBoardDto, but this should do for now
    public BoardDto Execute(BoardDto board)
    {
        var result = _repository.GetBySlug(board.Slug);
        if (result != null)
        {
            throw new Exception("Board already exists");
        }
        return _repository.Create(board.ToDomain()).ToDto();
    }
}