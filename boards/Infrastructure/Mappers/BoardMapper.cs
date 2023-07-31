using boards.Domain;
using boards.Infrastructure.Models;

namespace boards.Infrastructure.Mappers;

public static class BoardMapper
{
    public static BoardDomain ToDomain(this BoardDb db)
    {
        return new BoardDomain
        {
            Slug = db.Slug,
            Name = db.Name
        };
    }
    
    public static BoardDb ToDb(this BoardDomain domain)
    {
        return new BoardDb
        {
            Slug = domain.Slug,
            Name = domain.Name
        };
    }
}
