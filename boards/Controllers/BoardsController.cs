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
    public async Task<IEnumerable<BoardDto>> Get([FromServices] GetAllBoardsUseCase useCase, CancellationToken cancellationToken)
    {
        return await useCase.Execute(cancellationToken);
    }

    [HttpPost]
    public async Task<BoardDto?> Post([FromBody] BoardDto dto,
        [FromServices] CreateBoardUseCase useCase, CancellationToken cancellationToken)
    {
        return await useCase.Execute(dto, cancellationToken);
    }

    [HttpGet("{slug}")]
    public async Task<IActionResult> GetThreads([FromRoute] string slug,
        [FromServices] GetThreadsListUseCase useCase, CancellationToken cancellationToken)
    {
        var result = await useCase.Execute(slug, cancellationToken);
        if (result.IsFailed)
        {
            return NotFound();

        }

        return Ok(result.Value);
    }


    [HttpPost("{slug}/threads")]
    public async Task<ThreadDto?> PostThread([FromRoute] string slug,
        [FromBody] CreateThreadDto dto,
        [FromServices] CreateThreadUseCase useCase, CancellationToken cancellationToken)
    {
        return await useCase.Execute(slug, dto.Message, cancellationToken);
    }

    [HttpGet("{slug}/threads/{threadId:int}")]
    public async Task<ThreadDto?> GetThread([FromRoute] string slug,
        [FromRoute] int threadId,
        [FromServices] GetThreadUseCase useCase, CancellationToken cancellationToken)
    {
        return await useCase.Execute(slug, threadId, cancellationToken);
    }

    [HttpPost("{slug}/threads/{threadId:int}/replies")]
    public async Task<ReplyDto?> CreateReply([FromRoute] string slug,
        [FromRoute] int threadId,
        [FromBody] CreateReplyDto dto,
        [FromServices] CreateReplyUseCase useCase, CancellationToken cancellationToken)
    {
        return await useCase.Execute(slug, threadId, dto.Message, cancellationToken);
    }
}