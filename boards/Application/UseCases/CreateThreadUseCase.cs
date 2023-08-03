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

    public async Task<(ThreadDto?, IAppError?)> Execute(string boardSlug, string? title, string message,
        string? imageUrl, CancellationToken cancellationToken)
    {
        // TODO should this be injected?
        // TODO use fluent validation
        var messageError = ReplyValidation.ValidateReply(message);
        var imageError = UrlValidation.Validate(imageUrl);
        // pretty repetitive, but whatever :p
        if (messageError is not null)
        {
            return (null, messageError);
        }
        if (imageError is not null)
        {
            return (null, imageError);
        }
        
        var board = await _boardRepository.GetBySlug(boardSlug, cancellationToken);
        if (board is null)
        {
            return (null, new BoardDoesNotExistError());
        }

        var result = (await _boardRepository.CreateThread(boardSlug, title, message, imageUrl, cancellationToken))?.ToDto();
        return (result, null);
    }
}