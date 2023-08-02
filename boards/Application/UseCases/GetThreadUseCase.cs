using boards.Application.Dto;
using boards.Application.Mappers;
using boards.Domain.Repositories;

namespace boards.Application.UseCases;

public class GetThreadUseCase
{
    private readonly IBoardsRepository _boardsRepository;

    public GetThreadUseCase(IBoardsRepository boardsRepository)
    {
        _boardsRepository = boardsRepository;
    }

    public async Task<ThreadDto?> Execute(string slug, int threadId, CancellationToken cancellationToken)
    {
        var result = await _boardsRepository.GetThread(slug, threadId, cancellationToken);
        return result?.ToDto();
    }
}