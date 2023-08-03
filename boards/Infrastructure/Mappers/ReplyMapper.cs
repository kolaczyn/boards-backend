using boards.Domain.Models;
using boards.Infrastructure.Models;

namespace boards.Infrastructure.Mappers;

public static class ReplyMapper
{
    public static ReplyDomain ToDomain(this ReplyDb db)
    {
        return new ReplyDomain
        {
            Id = db.Id,
            Message = db.Message,
            CreatedAt = db.CreatedAt,
            ImageUrl = db.ImageUrl
        };
    }
}