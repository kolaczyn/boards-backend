using boards.Application.Dto;
using boards.Application.UseCases;
using boards.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace boards.Controllers;

[ApiController]
[Route("boards")]
public class BoardsController : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<BoardDto>> GetBoards([FromServices] GetAllBoardsUseCase useCase, CancellationToken cancellationToken)
    {
        return await useCase.Execute(cancellationToken);
    }

    [HttpPost]
    public async Task<BoardDto?> CreateBoard([FromBody] BoardDto dto,
        [FromServices] CreateBoardUseCase useCase, CancellationToken cancellationToken)
    {
        return await useCase.Execute(dto, cancellationToken);
    }

    [HttpGet("{slug}")]
    public async Task<BoardsThreadsDto?> GetThreads([FromRoute] string slug, [FromQuery] int page,
        [FromServices] GetThreadsListUseCase useCase, CancellationToken cancellationToken)
    {
        return await useCase.Execute(slug, page, cancellationToken);
    }


    [HttpPost("{slug}/threads")]
    public async Task<ThreadDto?> CreateThread([FromRoute] string slug,
        [FromBody] CreateThreadDto dto,
        [FromServices] CreateThreadUseCase useCase, CancellationToken cancellationToken)
    {
        return await useCase.Execute(slug, dto.Message, cancellationToken);
    }

    [HttpGet("{slug}/threads/{threadId:int}")]
    public async Task<ThreadDto?> GetThread([FromRoute] string slug, [FromRoute] int threadId,
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