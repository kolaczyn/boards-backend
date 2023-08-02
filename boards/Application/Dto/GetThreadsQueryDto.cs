using System.ComponentModel.DataAnnotations;

namespace boards.Application.Dto;

public class GetThreadsQueryDto
{
    [Required]
    public required int Page { get; init; } = 1;
    [Required]
    public required int PageSize { get; init; } = 24;
}