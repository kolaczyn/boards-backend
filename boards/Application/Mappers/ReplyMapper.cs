using boards.Application.Dto;
using boards.Domain;

namespace boards.Application.Mappers;

public static class ReplyMapper
{
    public static ReplyDto ToDto(this ReplyDomain replyDomain)
    {
        return new ReplyDto
        {
            Message = replyDomain.Message
        };
    }
    
    public static ReplyDomain ToDomain(this ReplyDto replyDto)
    {
        return new ReplyDomain
        {
            Message = replyDto.Message
        };
    }
}