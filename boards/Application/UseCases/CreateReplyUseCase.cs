using boards.Application.Dto;
using boards.Domain;

namespace boards.Application.UseCases;

public class CreateReplyUseCase
{
    private readonly IBoardsRepository _boardsRepository;

    public CreateReplyUseCase(IBoardsRepository boardsRepository)
    {
        _boardsRepository = boardsRepository;
    }

    public async Task<ReplyDto?> Execute(string slug, int threadId, string message)
    {
        var reply = await _boardsRepository.CreateReply(slug, threadId, message);
        if (reply == null)
        {
            return null;
        }

        return new ReplyDto
        {
            Message = reply.Message
        };
    }
}