using boards.Application.Dto;
using boards.Domain.Models;

namespace boards.Application.Mappers;

public static class CategoriesMapper
{
    public static CategoriesBoardsDto ToDto(this CategoriesBoardsDomain domain) =>
        new()
        {
            Id = domain.Id,
            Name = domain.Name,
            Boards = domain.Boards.Select(x => x.ToDto())
        };
}