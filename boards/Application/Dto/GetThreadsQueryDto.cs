namespace boards.Application.Dto;

public class GetThreadsQueryDto
{
    public required int Page { get; init; } = 1;
    public required int PageSize { get; init; } = 24;
    public required ThreadSortOrderDto SortOrder { get; init; } = ThreadSortOrderDto.ReplyCount;
}