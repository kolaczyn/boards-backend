using boards.Domain;
using boards.Infrastructure.Models;

namespace boards.Infrastructure.Mappers;

public static class ThreadMapper
{
    public static ThreadDomain ToDomain(this ThreadDb threadDb)
    {
        return new ThreadDomain
        {
            Replies = threadDb.Replies.Select(ReplyMapper.ToDomain),
            Board = BoardMapper.ToDomain(threadDb.Board)
        };
    }
}