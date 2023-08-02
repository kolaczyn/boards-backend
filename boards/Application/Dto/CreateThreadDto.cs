using System.ComponentModel.DataAnnotations;

namespace boards.Application.Dto;

public class CreateThreadDto
{
    [Required]
    public required string Message { get; init; }
}