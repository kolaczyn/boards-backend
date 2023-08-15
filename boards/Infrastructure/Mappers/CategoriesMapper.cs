using boards.Domain.Models;
using boards.Infrastructure.Models;

namespace boards.Infrastructure.Mappers;

public static class CategoriesMapper
{
    public static CategoriesBoardsDomain ToDomain(this CategoryDb db) =>
        new()
        {
            Id = db.Id,
            Name = db.Name,
            Boards = db.Boards.Select(x => x.ToDomain())
        };
}
