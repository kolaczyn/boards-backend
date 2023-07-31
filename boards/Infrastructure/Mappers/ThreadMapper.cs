using boards.Domain;
using boards.Infrastructure.Models;

namespace boards.Infrastructure.Mappers;

public static class ThreadMapper
{
    public static ThreadDomain ToDomain(this ThreadDb db)
    {
        return new ThreadDomain
        {
            Replies = db.Replies.Select(ReplyMapper.ToDomain),
            Board = db.Board.ToDomain(),
            Id = db.Id,
            CreatedAt = db.CreatedAt
        };
    }
}