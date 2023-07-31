namespace boards.Application.Dto;

public class ThreadDto
{
    public required int Id { get; set; }
    public required IEnumerable<ReplyDto> Replies { get; set; }
    public required DateTime? CreatedAt { get; set; }
}