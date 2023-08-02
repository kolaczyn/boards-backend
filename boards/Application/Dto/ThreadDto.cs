using System.ComponentModel.DataAnnotations;

namespace boards.Application.Dto;

public class ThreadDto
{
    [Required]
    public required int Id { get; init; }
    [Required]
    public required IEnumerable<ReplyDto> Replies { get; init; }
    public required DateTime? CreatedAt { get; init; }
}