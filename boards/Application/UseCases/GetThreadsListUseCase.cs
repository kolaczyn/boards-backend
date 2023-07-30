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

    public async Task<BoardsThreadsDto?> Execute(string boardSlug, CancellationToken cancellationToken)
    {
        var result = await _boardRepository.GetThreadsBySlug(boardSlug, cancellationToken);

        var boardThreads = result.FirstOrDefault()?.Board;

        // this is ugly af, but it should get the job done
        if (boardThreads is null)
        {
            var board = await _boardRepository.GetBySlug(boardSlug, cancellationToken);
            if (board is null)
            {
                return null;
            }

            return new BoardsThreadsDto
            {
                Slug = board.Slug,
                Name = board.Name,
                Threads = Enumerable.Empty<ThreadTeaserDto>()
            };
        }

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
}