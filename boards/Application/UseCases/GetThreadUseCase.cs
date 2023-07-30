using boards.Application.Dto;
using boards.Application.Mappers;
using boards.Domain;

namespace boards.Application.UseCases;

public class GetThreadUseCase
{
    private readonly IBoardsRepository _boardsRepository;

    public GetThreadUseCase(IBoardsRepository boardsRepository)
    {
        _boardsRepository = boardsRepository;
    }

    public ThreadDto? Execute(string slug, int threadId)
    {
        var result = this._boardsRepository.GetThread(slug, threadId);
        return result?.ToDto();
    }
}