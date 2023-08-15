namespace boards.Infrastructure.Models;

public class CategoryDb
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<BoardDb> Boards { get; set; }
}