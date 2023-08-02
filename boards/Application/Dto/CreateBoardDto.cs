using System.ComponentModel.DataAnnotations;

namespace boards.Application.Dto;

public class CreateBoardDto
{
    [Required]
    public required string Slug { get; init; }
    [Required]
    public required string Name { get; init; }
    [Required]
    public required string Password { get; init; }
}