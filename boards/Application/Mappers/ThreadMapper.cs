using boards.Application.Dto;
using boards.Domain;

namespace boards.Application.Mappers;

public static class ThreadMapper
{
    public static ThreadDto ToDto(this ThreadDomain domain)
    {
        return new ThreadDto
        {
            Replies = domain.Replies.Select(ReplyMapper.ToDto),
            Id = domain.Id,
            CreatedAt = domain.CreatedAt
        };
    }
}
