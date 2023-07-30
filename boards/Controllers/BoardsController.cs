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
    public async Task<IEnumerable<BoardDto>> Get([FromServices] GetAllBoardsUseCase useCase)
    {
        return await useCase.Execute();
    }

    [HttpPost]
    public async Task<BoardDto?> Post([FromBody] BoardDto dto,
        [FromServices] CreateBoardUseCase useCase)
    {
        return await useCase.Execute(dto);
    }

    [HttpGet("{slug}")]
    public async Task<BoardsThreadsDto?> GetThreads([FromRoute] string slug,
        [FromServices] GetThreadsListUseCase useCase)
    {
        return await useCase.Execute(slug);
    }


    [HttpPost("{slug}/threads")]
    public async Task<ThreadDto?> PostThread([FromRoute] string slug,
        [FromBody] CreateThreadDto dto,
        [FromServices] CreateThreadUseCase useCase)
    {
        return await useCase.Execute(slug, dto.Message);
    }

    [HttpGet("{slug}/threads/{threadId:int}")]
    public async Task<ThreadDto?> GetThread([FromRoute] string slug,
        [FromRoute] int threadId,
        [FromServices] GetThreadUseCase useCase)
    {
        return await useCase.Execute(slug, threadId);
    }

    [HttpPost("{slug}/threads/{threadId:int}/replies")]
    public async Task<ReplyDto?> CreateReply([FromRoute] string slug,
        [FromRoute] int threadId,
        [FromBody] CreateReplyDto dto,
        [FromServices] CreateReplyUseCase useCase)
    {
        return await useCase.Execute(slug, threadId, dto.Message);
    }
}