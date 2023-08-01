using boards.Application.Dto;
using boards.Application.Mappers;
using boards.Domain;
using boards.Domain.Errors;
using boards.Dto;

namespace boards.Application.UseCases;

public class CreateBoardUseCase
{
    private readonly IBoardsRepository _repository;
    private readonly ICheckPassword _checkPassword;
    public CreateBoardUseCase(IBoardsRepository repository, ICheckPassword checkPassword)
    {
        _repository = repository;
        _checkPassword = checkPassword;
    }
    
    // this should be CreateBoardDto, but this should do for now
    public async Task<(BoardDto?, IAppError?)> Execute(CreateBoardDto board, CancellationToken cancellationToken)
    {
        var validation = _checkPassword.Validate(board.Password);
        if (validation != CheckPasswordResult.Ok)
        {
            return (null, new WrongPasswordErr());
        }
        var foundBoard = await _repository.GetBySlug(board.Slug, cancellationToken);
        
        if (foundBoard is not null)
        {
            return (null, new BoardAlreadyExists());
        }

        var domain = new BoardDomain
        {
            Name = board.Name,
            Slug = board.Slug
        };
        var resultDto = (await _repository.Create(domain, cancellationToken))?.ToDto();
        return (resultDto, null);
    }
}