using System.ComponentModel.DataAnnotations;

namespace boards.Application.Dto;

public class CreateThreadDto
{
    public string? Title { get; init; }
    [Required]
    public required string Message { get; init; }
}