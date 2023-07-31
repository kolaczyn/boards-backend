namespace boards.Domain;

public class BoardsThreadsDomain
{
    public required string Slug { get; set; }
    public required string Name { get; set; }
    public required IEnumerable<ThreadTeaserDomain> Threads { get; set; }
}