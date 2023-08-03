using boards.Application.Dto;
using boards.Domain.Models;

namespace boards.Application.Mappers;

public static class ReplyMapper
{
    public static ReplyDto ToDto(this ReplyDomain domain) =>
        new()
        {
            Message = domain.Message,
            CreatedAt = domain.CreatedAt,
            Id = domain.Id,
            ImageUrl = domain.ImageUrl
        };
}