namespace boards.Application.Dto;

public class GetThreadsQueryDto
{
    required public int Page { get; set; } = 1;
    required public int PageSize { get; set; } = 24;
}