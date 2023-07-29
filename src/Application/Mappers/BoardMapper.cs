using boards.Dto;

namespace boards.Application;

public static class BoardMapper
{
    public static BoardDto ToDto(this BoardDomain board)
    {
        return new BoardDto
        {
            Slug = board.Slug,
            Name = board.Name
        };
    }
    
    public static BoardDomain ToDomain(this BoardDto board)
    {
        return new BoardDomain
        {
            Slug = board.Slug,
            Name = board.Name
        };
    }
}