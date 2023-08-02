using System.ComponentModel.DataAnnotations;

namespace boards.Application.Dto;

public class PasswordProtectionDto
{
    [Required]
    public required string Password { get; init; }
}