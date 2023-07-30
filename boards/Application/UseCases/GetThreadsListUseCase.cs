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

    public BoardsThreadsDto Execute(string boardSlug)
    {
        var result = _boardRepository.GetThreadsBySlug(boardSlug);

        var board = result.First().Board;
        
        return new BoardsThreadsDto
        {
            Slug = board.Slug,
            Name = board.Name,
            Threads = result.Select(x => new ThreadTeaserDto
            {
                Id = x.Id,
                Message = x.Replies.First().Message,
                RepliesCount = x.Replies.Count()
            })
        };
    }
}