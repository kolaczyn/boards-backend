namespace boards.Domain.Models;

public class CategoriesBoardsDomain
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required IEnumerable<BoardDomain> Boards { get; init; }
}