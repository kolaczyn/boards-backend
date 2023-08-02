using boards.Domain.Models;
using boards.Dto;

namespace boards.Application.Mappers;

public static class BoardMapper
{
    public static BoardDto ToDto(this BoardDomain domain) =>
        new()
        {
            Slug = domain.Slug,
            Name = domain.Name
        };
}