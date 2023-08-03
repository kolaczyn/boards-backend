using boards.Application.Dto;
using boards.Application.Mappers;
using boards.Domain.Repositories;
using boards.Domain.Errors;
using boards.Domain.Validation;

namespace boards.Application.UseCases;

public class CreateThreadUseCase
{
    private readonly IBoardsRepository _boardRepository;

    public CreateThreadUseCase(IBoardsRepository boardRepository)
    {
        _boardRepository = boardRepository;
    }

    public async Task<(ThreadDto?, IAppError?)> Execute(string boardSlug, string?  title, string message,
        CancellationToken cancellationToken)
    {
        // TODO should this be injected?
        var validationError = ReplyValidation.ValidateReply(message);
        if (validationError is not null)
        {
            return (null, validationError);
        }
        var board = await _boardRepository.GetBySlug(boardSlug, cancellationToken);
        if (board is null)
        {
            return (null, new BoardDoesNotExistError());
        }

        var result = (await _boardRepository.CreateThread(boardSlug, title, message, cancellationToken))?.ToDto();
        return (result, null);
    }
}