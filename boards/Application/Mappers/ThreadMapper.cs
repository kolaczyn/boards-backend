using boards.Application.Dto;
using boards.Domain.Models;

namespace boards.Application.Mappers;

public static class ThreadMapper
{
    public static ThreadDto ToDto(this ThreadDomain domain) =>
        new()
        {
            Replies = domain.Replies.Select(ReplyMapper.ToDto),
            Id = domain.Id,
            CreatedAt = domain.CreatedAt
        };
}
