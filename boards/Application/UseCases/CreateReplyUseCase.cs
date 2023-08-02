using boards.Application.Dto;
using boards.Domain.Repositories;

namespace boards.Application.UseCases;

public class CreateReplyUseCase
{
    private readonly IBoardsRepository _boardsRepository;

    public CreateReplyUseCase(IBoardsRepository boardsRepository)
    {
        _boardsRepository = boardsRepository;
    }

    public async Task<ReplyDto?> Execute(string slug, int threadId, string message, CancellationToken cancellationToken)
    {
        var reply = await _boardsRepository.CreateReply(slug, threadId, message, cancellationToken);
        if (reply == null)
        {
            return null;
        }

        return new ReplyDto
        {
            Id = reply.Id,
            Message = reply.Message,
            CreatedAt = reply.CreatedAt
        };
    }
}