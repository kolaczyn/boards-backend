using boards.Application.Dto;
using boards.Domain.Errors;
using boards.Domain.Repositories;

namespace boards.Application.UseCases;

public class GetThreadsListUseCase
{
    private readonly IBoardsRepository _boardRepository;

    public GetThreadsListUseCase(IBoardsRepository boardRepository)
    {
        _boardRepository = boardRepository;
    }

    public async Task<(BoardsThreadsDto?, IAppError?)> Execute(string boardSlug, int page, int pageSize,
        ThreadSortOrderDto sortOrder, CancellationToken cancellationToken)
    {
        var (result, err) = await _boardRepository.GetThreads(boardSlug, page, pageSize, cancellationToken);

        if (result is null)
        {
            return (null, err);
        }

        var dto = new BoardsThreadsDto
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

        return (dto, null);
    }
}