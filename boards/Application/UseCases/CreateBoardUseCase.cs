using boards.Application.Mappers;
using boards.Domain;
using boards.Domain.Errors;
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
    public async Task<(BoardDto?, AppError?)> Execute(BoardDto board, CancellationToken cancellationToken)
    {
        var foundBoard = await _repository.GetBySlug(board.Slug, cancellationToken);
        
        if (foundBoard is not null)
        {
            return (null, new BoardAlreadyExists());
        }
        
        var dto = (await _repository.Create(board.ToDomain(), cancellationToken))?.ToDto();
        return (dto, null);
    }
}