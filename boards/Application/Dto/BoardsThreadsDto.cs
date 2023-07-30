
namespace boards.Application.Dto;

public class BoardsThreadsDto
{
    public string Slug { get; set; }
    public string Name { get; set; }
    public IEnumerable<ThreadTeaserDto> Threads { get; set; }
}