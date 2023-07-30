using boards.Application.Dto;
using boards.Application.UseCases;
using boards.Dto;
using Microsoft.AspNetCore.Mvc;

namespace boards.Controllers;

[ApiController]
[Route("boards")]
public class BoardsController : ControllerBase
{
    [HttpGet]
    public IEnumerable<BoardDto> Get([FromServices] GetAllBoardsUseCase useCase)
    {
        return useCase.Execute();
    }

    [HttpPost]
    public BoardDto Post([FromBody] BoardDto dto,
        [FromServices] CreateBoardUseCase useCase)
    {
        return useCase.Execute(dto);
    }

    [HttpGet("{slug}")]
    public BoardsThreadsDto GetThreads([FromRoute] string slug,
        [FromServices] GetThreadsListUseCase useCase)
    {
        return useCase.Execute(slug);
    }


    [HttpPost("{slug}/threads")]
    public ThreadDto? PostThread([FromRoute] string slug,
        [FromBody] CreateThreadDto dto,
        [FromServices] CreateThreadUseCase useCase)
    {
        return useCase.Execute(slug, dto.Message);
    }

    [HttpGet("{slug}/threads/{threadId:int}")]
    public ThreadDto? GetThread([FromRoute] string slug,
        [FromRoute] int threadId,
        [FromServices] GetThreadUseCase useCase)
    {
        return useCase.Execute(slug, threadId);
    }

    [HttpPost("{slug}/threads/{threadId:int}/replies")]
    public ReplyDto? CreateReply([FromRoute] string slug,
        [FromRoute] int threadId,
        [FromBody] CreateReplyDto dto,
        [FromServices] CreateReplyUseCase useCase)
    {
        return useCase.Execute(slug, threadId, dto.Message);
    }
}