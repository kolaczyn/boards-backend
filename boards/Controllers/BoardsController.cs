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
    public async Task<IEnumerable<BoardDto>> GetBoards([FromServices] GetAllBoardsUseCase useCase,
        CancellationToken cancellationToken)
    {
        return await useCase.Execute(cancellationToken);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBoard([FromBody] CreateBoardDto dto,
        [FromServices] CreateBoardUseCase useCase, CancellationToken cancellationToken)
    {
        var (response, err) = await useCase.Execute(dto, cancellationToken);

        if (response is null)
        {
            return err switch
            {
                BoardAlreadyExists => BadRequest(err.Message),
                WrongPasswordErr => Unauthorized(err.Message),
                _ => StatusCode((int)HttpStatusCode.InternalServerError)
            };
        }

        return Created($"/boards/{response.Slug}",response);
    }

    [HttpGet("{slug}")]
    public async Task<IActionResult> GetThreads([FromRoute] string slug,
        [FromQuery] GetThreadsQueryDto query,
        [FromServices] GetThreadsListUseCase useCase, CancellationToken cancellationToken
    )
    {
        var (response, err) = await useCase.Execute(slug, query.Page, query.PageSize, cancellationToken);

        if (err is not null)
        {
            return err switch
            {
                BoardDoesNotExistError => NotFound(err.Message),
                _ => StatusCode((int)HttpStatusCode.InternalServerError)
            };
        }

        return Ok(response);
    }


    [HttpPost("{slug}/threads")]
    public async Task<IActionResult> CreateThread([FromRoute] string slug,
        [FromBody] CreateThreadDto dto,
        [FromServices] CreateThreadUseCase useCase, CancellationToken cancellationToken)
    {
        var (response, err) = await useCase.Execute(slug, dto.Message, cancellationToken);

        if (err is not null)
        {
            return err switch
            {
                BoardDoesNotExistError => NotFound(err.Message),
                ReplyTooShort => BadRequest(err.Message),
                _ => StatusCode((int)HttpStatusCode.InternalServerError)
            };
        }

        return Ok(response);
    }

    [HttpGet("{slug}/threads/{threadId:int}")]
    public async Task<IActionResult> GetThread([FromRoute] string slug, [FromRoute] int threadId,
        [FromServices] GetThreadUseCase useCase, CancellationToken cancellationToken)
    {
        var response = await useCase.Execute(slug, threadId, cancellationToken);
        if (response is null)
        {
            return Ok();
        }

        return Ok(response);
    }

    [HttpPost("{slug}/threads/{threadId:int}/replies")]
    public async Task<ReplyDto?> CreateReply([FromRoute] string slug,
        [FromRoute] int threadId,
        [FromBody] CreateReplyDto dto,
        [FromServices] CreateReplyUseCase useCase, CancellationToken cancellationToken)
    {
        return await useCase.Execute(slug, threadId, dto.Message, cancellationToken);
    }

    [HttpDelete("{slug}/threads/{threadId:int}/replies/{replyId:int}")]
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
                ReplyDoesNotExist => NotFound(err.Message),
                WrongPasswordErr => Unauthorized(err.Message),
                _ => StatusCode((int)HttpStatusCode.InternalServerError)
            };
        }

        return Ok(response);
    }
}