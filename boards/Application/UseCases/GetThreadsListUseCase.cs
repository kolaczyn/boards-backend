using boards.Application.Dto;
using boards.Domain;
using FluentResults;

namespace boards.Application.UseCases;

public class GetThreadsListUseCase
{
    private readonly IBoardsRepository _boardRepository;

    public GetThreadsListUseCase(IBoardsRepository boardRepository)
    {
        _boardRepository = boardRepository;
    }

    public async Task<Result<BoardsThreadsDto>> Execute(string boardSlug, CancellationToken cancellationToken)
    {
        var result = await _boardRepository.GetThreadsBySlug(boardSlug, cancellationToken);

        var boardThreads = result.FirstOrDefault()?.Board;

        if (boardThreads is not null)
        {
            return new BoardsThreadsDto
            {
                Slug = boardThreads.Slug,
                Name = boardThreads.Name,
                Threads = result.Select(x => new ThreadTeaserDto
                {
                    Id = x.Id,
                    Message = x.Replies.First().Message,
                    RepliesCount = x.Replies.Count()
                })
            };
        }

        var board = await _boardRepository.GetBySlug(boardSlug, cancellationToken);
        if (board.IsFailed)
        {
            return Result.Fail(new BoardDoesNotExist());
        }

        return new BoardsThreadsDto
        {
            Slug = board.Value.Slug,
            Name = board.Value.Name,
            Threads = Enumerable.Empty<ThreadTeaserDto>()
        };
    }
}