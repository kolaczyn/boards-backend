using boards.Application.Dto;
using boards.Application.Mappers;
using boards.Domain;

namespace boards.Application.UseCases;

public class GetThreadsListUseCase
{
    private readonly IBoardsRepository _boardRepository;

    public GetThreadsListUseCase(IBoardsRepository boardRepository)
    {
        _boardRepository = boardRepository;
    }

    public async Task<BoardsThreadsDto?> Execute(string boardSlug, int page, CancellationToken cancellationToken)
    {
        var result = await _boardRepository.GetThreads(boardSlug, page, cancellationToken);

        if (result is null)
        {
            return null;
        }

        return new BoardsThreadsDto
        {
            Name = result.Name,
            Slug = result.Slug,
            Threads = result.Threads.Select(x => new ThreadTeaserDto
            {
                Id = x.Id,
                Message = x.Message,
                RepliesCount = x.RepliesCount,
                CreatedAt = x.CreatedAt
            })
        };
    }
}