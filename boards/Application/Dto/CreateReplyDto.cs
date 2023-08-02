using System.ComponentModel.DataAnnotations;

namespace boards.Application.Dto;

public class CreateReplyDto
{
    [Required]
    public required string Message { get; init; }
}
