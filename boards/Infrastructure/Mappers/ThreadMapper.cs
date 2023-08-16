using boards.Domain.Models;
using boards.Infrastructure.Models;

namespace boards.Infrastructure.Mappers;

public static class ThreadMapper
{
    public static ThreadDomain ToDomain(this ThreadDb db) =>
        new()
        {
            Replies = db.Replies.Select(ReplyMapper.ToDomain),
            Board = db.Board.ToDomain(),
            Id = db.Id,
            CreatedAt = db.CreatedAt,
            Title = db.Title,
        };
}