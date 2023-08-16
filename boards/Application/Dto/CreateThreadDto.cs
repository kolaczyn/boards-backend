using System.ComponentModel.DataAnnotations;

namespace boards.Application.Dto;

public class CreateThreadDto
{
    public string? Title { get; init; }
    [Required]
    public required string Message { get; init; }
    public string? ImageUrl { get; init; }
    public string? Tripcode { get; init; }
}
