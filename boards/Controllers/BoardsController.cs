using System.Net;
using boards.Application.Dto;
using boards.Application.UseCases;
using boards.Domain.Errors;
using boards.Dto;
using Microsoft.AspNetCore.Mvc;

namespace boards.Controllers;

[ApiController]
[Route("boards")]
public class BoardsController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BoardDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBoards([FromServices] GetAllBoardsUseCase useCase,
        CancellationToken cancellationToken) =>
        Ok(await useCase.Execute(cancellationToken));

    [HttpPost]
    [ProducesResponseType(typeof(BoardDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateBoard([FromBody] CreateBoardDto dto,
        [FromServices] CreateBoardUseCase useCase, CancellationToken cancellationToken)
    {
        var (response, err) = await useCase.Execute(dto, cancellationToken);

        if (response is null)
        {
            return err switch
            {
                BoardAlreadyExists => BadRequest(err),
                WrongPasswordErr => Unauthorized(err),
                _ => StatusCode((int)HttpStatusCode.InternalServerError)
            };
        }

        return Created($"/boards/{response.Slug}", response);
    }

    
    [HttpGet("{slug}")]
    [ProducesResponseType(typeof(BoardsThreadsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AppError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetThreads([FromRoute] string slug,
        [FromQuery] GetThreadsQueryDto query,
        [FromServices] GetThreadsListUseCase useCase, CancellationToken cancellationToken
    )
    {
        var (response, err) = await useCase.Execute(slug,
            query.Page, query.PageSize, query.SortOrder, cancellationToken);

        if (err is not null)
        {
            return err switch
            {
                BoardDoesNotExistError => NotFound(err),
                _ => StatusCode((int)HttpStatusCode.InternalServerError)
            };
        }

        return Ok(response);
    }


    [HttpPost("{slug}/threads")]
    [ProducesResponseType(typeof(ThreadDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(AppError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateThread([FromRoute] string slug, [FromBody] CreateThreadDto dto,
        [FromServices] CreateThreadUseCase useCase, CancellationToken cancellationToken)
    {
        var (response, err) = await useCase.Execute(slug, dto.Title, dto.Message, dto.ImageUrl, cancellationToken);

        if (response is null)
        {
            return err switch
            {
                BoardDoesNotExistError => NotFound(err),
                ReplyTooShort => BadRequest(err),
                FieldIsNotUrl => BadRequest(err),
                _ => StatusCode((int)HttpStatusCode.InternalServerError)
            };
        }

        return Created($"/boards/{slug}/threads/{response.Id}", response);
    }

    [HttpGet("{slug}/threads/{threadId:int}")]
    [ProducesResponseType(typeof(ThreadDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AppError),StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetThread([FromRoute] string slug, [FromRoute] int threadId,
        [FromServices] GetThreadUseCase useCase, CancellationToken cancellationToken)
    {
        var response = await useCase.Execute(slug, threadId, cancellationToken);
        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpPost("{slug}/threads/{threadId:int}/replies")]
    [ProducesResponseType(typeof(ReplyDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(AppError),StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateReply([FromRoute] string slug,
        [FromRoute] int threadId,
        [FromBody] CreateReplyDto dto,
        [FromServices] CreateReplyUseCase useCase, CancellationToken cancellationToken)
    {
        var response = await useCase.Execute(slug, threadId, dto.Message, cancellationToken);

        if (response is null)
        {
            return NotFound();
        }

        return Created($"/boards/{slug}/threads/{threadId}", response);
    }

    [HttpDelete("{slug}/threads/{threadId:int}/replies/{replyId:int}")]
    [ProducesResponseType(typeof(ReplyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AppError), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteReply([FromRoute] string slug,
        [FromRoute] int threadId,
        [FromRoute] int replyId,
        [FromBody] PasswordProtectionDto dto,
        [FromServices] DeleteReplyUseCase useCase, CancellationToken cancellationToken)
    {
        var (response, err) = await useCase.Execute(slug, threadId, replyId, dto.Password, cancellationToken);

        if (response is null)
        {
            return err switch
            {
                ReplyDoesNotExist => NotFound(err),
                WrongPasswordErr => Unauthorized(err),
                _ => StatusCode((int)HttpStatusCode.InternalServerError)
            };
        }

        return Ok(response);
    }
}