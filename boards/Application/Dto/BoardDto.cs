using System.ComponentModel.DataAnnotations;

namespace boards.Dto;

public class BoardDto
{
    [Required]
    public required string Slug { get; init; }
    [Required]
    public required string Name { get; init; }
}