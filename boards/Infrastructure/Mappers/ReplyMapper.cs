using boards.Domain;
using boards.Infrastructure.Models;

namespace boards.Infrastructure.Mappers;

public static class ReplyMapper
{
    public static ReplyDomain ToDomain(this ReplyDb db)
    {
        return new ReplyDomain
        {
            Message = db.Message,
            CreatedAt = db.CreatedAt
        };
    }
}