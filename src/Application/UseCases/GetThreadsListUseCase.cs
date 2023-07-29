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

    public IEnumerable<ThreadDto> Execute(string boardSlug)
    {
        return this._boardRepository.GetThreadsBySlug(boardSlug).Select(x => x.ToDto());
    }
}