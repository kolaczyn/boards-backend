
using System.ComponentModel.DataAnnotations;

namespace boards.Application.Dto;

public class BoardsThreadsDto
{
    [Required]
    public required string Slug { get; init; }
    [Required]
    public required string Name { get; init; }
    [Required]
    public required IEnumerable<ThreadTeaserDto> Threads { get; init; }
}