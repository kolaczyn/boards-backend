using boards.Application.Dto;
using boards.Domain.Errors;
using boards.Domain.Providers;
using boards.Domain.Repositories;
using boards.Domain.Validation;

namespace boards.Application.UseCases;

public class CreateReplyUseCase
{
    private readonly IBoardsRepository _boardsRepository;
    private readonly ITripcodeEncoder _tripcodeEncoder;

    public CreateReplyUseCase(IBoardsRepository boardsRepository, ITripcodeEncoder tripcodeEncoder)
    {
        _boardsRepository = boardsRepository;
        _tripcodeEncoder = tripcodeEncoder;
    }

    public async Task<(ReplyDto?, IAppError?)> Execute(int threadId, CreateReplyDto dto, CancellationToken cancellationToken)
    {
        var validationErr = UrlValidation.Validate(dto.ImageUrl);
        
        // we allow null images
        if (validationErr is not null && dto.ImageUrl is not null)
        {
            return (null, validationErr);
        }
        
        if (dto.Tripcode is not null && _tripcodeEncoder.Encode(dto.Tripcode) is null)
        {
            return (null, new InvalidTripcode());
        }

        var tripcode  = dto.Tripcode is not null ? _tripcodeEncoder.Encode(dto.Tripcode) : null;
        var (reply, err) = await _boardsRepository.CreateReply(threadId, dto.Message, dto.ImageUrl, tripcode, cancellationToken);
        if (reply is null)
        {
            return (null, err);
        }
        
        var result =  new ReplyDto
        {
            Id = reply.Id,
            Message = reply.Message,
            CreatedAt = reply.CreatedAt,
            ImageUrl = reply.ImageUrl,
            Tripcode = reply.Tripcode
        };

        return (result, null);
    }
}