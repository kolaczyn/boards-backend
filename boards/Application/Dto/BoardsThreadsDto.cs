
namespace boards.Application.Dto;

public class BoardsThreadsDto
{
    public required string Slug { get; set; }
    public required string Name { get; set; }
    public required IEnumerable<ThreadTeaserDto> Threads { get; set; }
}