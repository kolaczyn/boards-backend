using boards.Application;
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

    [HttpGet("{slug}")]
    public IActionResult Get([FromRoute] string slug, [FromServices] GetBoardBySlugUseCase useCase)
    {
        var result = useCase.Execute(slug);
        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }
    
    [HttpPost]
    public BoardDto Post([FromBody] BoardDto dto, [FromServices] CreateBoardUseCase useCase)
    {
        var result = useCase.Execute(dto);
        return result;
    }

    [HttpGet("{slug}/threads")]
    public IEnumerable<ThreadDto> GetThreads([FromRoute] string slug, [FromServices] GetThreadsListUseCase useCase)
    {
        return useCase.Execute(slug);
    }
}
