namespace boards.Application.Dto;

public class ThreadDto
{
    public int Id { get; set; }
    public IEnumerable<ReplyDto> Replies { get; set; }
}