using boards.Application.Dto;
using boards.Domain;

namespace boards.Application.Mappers;

public static class ThreadMapper
{
    public static ThreadDto ToDto(this ThreadDomain threadDomain)
    {
        return new ThreadDto
        {
            Replies = threadDomain.Replies.Select(ReplyMapper.ToDto),
            Id = threadDomain.Id
        };
    }
}
