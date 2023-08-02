using System.ComponentModel.DataAnnotations;

namespace boards.Application.Dto;

public class ThreadTeaserDto
{
    [Required]
    public required int Id { get; init; }
    [Required]
    public required string Message { get; init; }
    [Required]
    public required int RepliesCount { get; init; }
    public required DateTime? CreatedAt { get; init; }
}