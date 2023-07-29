using boards.Dto;
using boards.Infrastructure;

namespace boards.Mappers;

public static class BoardsMapper
{
    public static BoardDto ToDto(this BoardDb board)
    {
        return new BoardDto
        {
            Slug = board.Slug,
            Name = board.Name
        };
    }
}