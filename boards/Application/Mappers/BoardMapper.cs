using boards.Domain.Models;
using boards.Dto;

namespace boards.Application.Mappers;

public static class BoardMapper
{
    public static BoardDto ToDto(this BoardDomain domain)
    {
        return new BoardDto
        {
            Slug = domain.Slug,
            Name = domain.Name
        };
    }
    
    public static BoardDomain ToDomain(this BoardDto dto)
    {
        return new BoardDomain
        {
            Slug = dto.Slug,
            Name = dto.Name
        };
    }
}