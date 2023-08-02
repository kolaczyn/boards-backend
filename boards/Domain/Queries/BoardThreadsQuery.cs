namespace boards.Domain.Queries;

public class BoardThreadsQuery
{
    public required string Slug { get; init; }
    public required int Page { get; init; }
    public required int PageSize { get; init; }
    public required ThreadSortOrder SortOrder { get; init; }
}