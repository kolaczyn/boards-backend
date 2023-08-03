using System.ComponentModel.DataAnnotations;

namespace boards.Application.Dto;

public class ReplyDto
{
    [Required]
    public required int Id { get; init; }
    [Required]
    public required string Message { get; init; }
    public DateTime? CreatedAt { get; init; }
}