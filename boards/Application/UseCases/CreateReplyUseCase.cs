using boards.Application.Dto;
using boards.Domain.Errors;
using boards.Domain.Repositories;
using boards.Domain.Validation;

namespace boards.Application.UseCases;

public class CreateReplyUseCase
{
    private readonly IBoardsRepository _boardsRepository;

    public CreateReplyUseCase(IBoardsRepository boardsRepository)
    {
        _boardsRepository = boardsRepository;
    }

    public async Task<(ReplyDto?, IAppError?)> Execute(int threadId, string message, string? imageUrl, CancellationToken cancellationToken)
    {
        var validationErr = UrlValidation.Validate(imageUrl);
        
        // we allow null images
        if (validationErr is not null && imageUrl is not null)
        {
            return (null, validationErr);
        }
        var (reply, err) = await _boardsRepository.CreateReply(threadId, message, imageUrl, cancellationToken);
        if (reply is null)
        {
            return (null, err);
        }

        var result =  new ReplyDto
        {
            Id = reply.Id,
            Message = reply.Message,
            CreatedAt = reply.CreatedAt,
            ImageUrl = reply.ImageUrl
        };

        return (result, null);
    }
}