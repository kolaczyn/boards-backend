namespace boards.Infrastructure.Models;

public class BoardDb
{
    public string Slug { get; set; }
    public string Name { get; set; }
    public List<ThreadDb> Threads { get; set; }
}