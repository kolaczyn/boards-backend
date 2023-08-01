namespace boards.Application.Dto;

public class ReplyDto
{
    public required int Id { get; set; }
    public required string Message { get; set; }
    public required DateTime? CreatedAt { get; set; }
}