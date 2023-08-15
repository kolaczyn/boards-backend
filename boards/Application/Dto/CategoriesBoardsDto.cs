using System.ComponentModel.DataAnnotations;
using boards.Dto;

namespace boards.Application.Dto;

public class CategoriesBoardsDto
{
    [Required]
    public required int Id { get; init; }
    [Required]
    public required string Name { get; init; }
    [Required]
    public required IEnumerable<BoardDto> Boards { get; init; }
}