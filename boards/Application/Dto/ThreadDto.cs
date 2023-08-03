using System.ComponentModel.DataAnnotations;

namespace boards.Application.Dto;

public sealed class ThreadDto
{
    [Required]
    public required int Id { get; init; }
    public string? Title { get; init; }
    [Required]
    public required IEnumerable<ReplyDto> Replies { get; init; }
    public required DateTime? CreatedAt { get; init; }
}