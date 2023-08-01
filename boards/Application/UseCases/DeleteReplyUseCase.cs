using boards.Application.Dto;
using boards.Application.Mappers;
using boards.Domain;
using boards.Domain.Errors;

namespace boards.Application.UseCases;

public class DeleteReplyUseCase
{
    private readonly IBoardsRepository _boardsRepository;
    private readonly ICheckPassword _checkPassword;

    public DeleteReplyUseCase(IBoardsRepository boardsRepository, ICheckPassword checkPassword)
    {
        _boardsRepository = boardsRepository;
        _checkPassword = checkPassword;
    }
    
    public async Task<(ReplyDto?, IAppError?)> Execute(string slug, int threadId, int replyId, string password, CancellationToken cancellationToken)
    {
        if (_checkPassword.Validate(password) == CheckPasswordResult.WrongPassword)
        {
            return (null, new WrongPasswordErr());
        }
        
        var reply = await _boardsRepository.GetReply(slug, threadId, replyId, cancellationToken);
        if (reply is null)
        {
            return (null, new ReplyDoesNotExist());
        }
        var result = await _boardsRepository.DeleteReply(slug, threadId, replyId, cancellationToken);

        if (result is null)
        {
            return (null, new SomethingWentWrongErr());
        }

        return (result.ToDto(), null);
    }
}