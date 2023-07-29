namespace boards.Infrastructure;

public static class BoardMapper
{
    public static BoardDomain ToDomain(this BoardDb board)
    {
        return new BoardDomain
        {
            Slug = board.Slug,
            Name = board.Name
        };
    }
    
    public static BoardDb ToDb(this BoardDomain board)
    {
        return new BoardDb
        {
            Slug = board.Slug,
            Name = board.Name
        };
    }
}
