using boards.Application.Dto;
using boards.Domain;

namespace boards.Application.Mappers;

public static class ReplyMapper
{
    public static ReplyDto ToDto(this ReplyDomain domain)
    {
        return new ReplyDto
        {
            Message = domain.Message,
            CreatedAt = domain.CreatedAt
        };
    }
}