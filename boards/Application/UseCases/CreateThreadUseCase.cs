using boards.Application.Dto;
using boards.Application.Mappers;
using boards.Domain;

namespace boards.Application.UseCases;

public class CreateThreadUseCase
{
    private readonly IBoardsRepository _boardRepository;
   public  CreateThreadUseCase(IBoardsRepository boardRepository)
    {
        _boardRepository = boardRepository;
    }
    
    public ThreadDto? Execute(string boardSlug, string message)
    {
        var board = _boardRepository.GetBySlug(boardSlug);
        if (board == null)
        {
            return null;
        }
        return _boardRepository.CreateThread(boardSlug, message)?.ToDto();
    } 
}