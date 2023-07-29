using boards.Dto;

namespace boards.Application.Dto;

public class ThreadDto
{
    public IEnumerable<ReplyDto> Replies { get; set; }
    public BoardDto Board { get; set; }
}