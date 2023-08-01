namespace boards.Application.Dto;

public class CreateBoardDto
{
    public required string Slug { get; set; }
    public required string Name { get; set; }
    public required string Password { get; set; }
}