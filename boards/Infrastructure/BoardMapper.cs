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
    
}
