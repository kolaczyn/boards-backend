using boards.Domain;
using boards.Infrastructure.Models;

namespace boards.Infrastructure.Mappers;

public static class ReplyMapper
{
    public static ReplyDomain ToDomain(this ReplyDb replyDb)
    {
        return new ReplyDomain
        {
            Message = replyDb.Message
        };
    }
}