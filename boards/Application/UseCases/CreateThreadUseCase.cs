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
    
    public async Task<ThreadDto?> Execute(string boardSlug, string message, CancellationToken cancellationToken)
    {
        var board = await _boardRepository.GetBySlug(boardSlug, cancellationToken);
        if (board == null)
        {
            return null;
        }
        return (await _boardRepository.CreateThread(boardSlug, message, cancellationToken))?.ToDto();
    } 
}