using boards.Application.Dto;
using boards.Application.Mappers;
using boards.Domain.Repositories;
using boards.Domain.Errors;
using boards.Domain.Providers;
using boards.Domain.Validation;

namespace boards.Application.UseCases;

public class CreateThreadUseCase
{
    private readonly IBoardsRepository _boardRepository;
    private readonly ITripcodeEncoder _tripcodeEncoder;

    public CreateThreadUseCase(IBoardsRepository boardRepository, ITripcodeEncoder tripcodeEncoder)
    {
        _boardRepository = boardRepository;
        _tripcodeEncoder = tripcodeEncoder;
    }

    public async Task<(ThreadDto?, IAppError?)> Execute(string boardSlug, CreateThreadDto dto,
         CancellationToken cancellationToken)
    {
        // TODO should this be injected?
        // TODO use fluent validation
        // TODO this is getting out of hand, I should do something about it
        // lol
        var messageError = ReplyValidation.ValidateReply(dto.Message);
        var imageError = UrlValidation.Validate(dto.ImageUrl);
        // pretty repetitive, but whatever :p
        if (messageError is not null)
        {
            return (null, messageError);
        }
        if (imageError is not null)
        {
            return (null, imageError);
        }

        if (dto.Tripcode is not null && _tripcodeEncoder.Encode(dto.Tripcode) is null)
        {
            return (null, new InvalidTripcode());
        }
        
        var board = await _boardRepository.GetBySlug(boardSlug, cancellationToken);
        if (board is null)
        {
            return (null, new BoardDoesNotExistError());
        }

        var tripcode  = dto.Tripcode is not null ? _tripcodeEncoder.Encode(dto.Tripcode) : null;
        var result = (await _boardRepository.CreateThread(boardSlug, dto.Title, dto.Message, dto.ImageUrl, tripcode, cancellationToken))?.ToDto();
        return (result, null);
    }
}