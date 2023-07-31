using boards.Application.Dto;
using boards.Application.Mappers;
using boards.Domain;
using boards.Domain.Errors;

namespace boards.Application.UseCases;

public class CreateThreadUseCase
{
    private readonly IBoardsRepository _boardRepository;

    public CreateThreadUseCase(IBoardsRepository boardRepository)
    {
        _boardRepository = boardRepository;
    }

    public async Task<(ThreadDto?, AppError?)> Execute(string boardSlug, string message,
        CancellationToken cancellationToken)
    {
        var board = await _boardRepository.GetBySlug(boardSlug, cancellationToken);
        if (board is null)
        {
            return (null, new BoardDoesNotExistErrors());
        }

        var result = (await _boardRepository.CreateThread(boardSlug, message, cancellationToken))?.ToDto();
        return (result, null);
    }
}