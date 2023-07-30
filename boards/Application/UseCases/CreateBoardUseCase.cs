using boards.Application.Mappers;
using boards.Domain;
using boards.Dto;

namespace boards.Application.UseCases;

public class CreateBoardUseCase
{
    private readonly IBoardsRepository _repository;
    public CreateBoardUseCase(IBoardsRepository repository)
    {
        _repository = repository;
    }
    
    // this should be CreateBoardDto, but this should do for now
    public async Task<BoardDto?> Execute(BoardDto board, CancellationToken cancellationToken)
    {
        var result = await _repository.GetBySlug(board.Slug, cancellationToken);
        if (result != null)
        {
            throw new Exception("Board already exists");
        }
        return (await _repository.Create(board.ToDomain(), cancellationToken))?.ToDto();
    }
}