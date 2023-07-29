using boards.Application;
using boards.Dto;
using Microsoft.AspNetCore.Mvc;

namespace boards.Controllers;

[ApiController]
[Route("[controller]")]
public class BoardsController : ControllerBase
{
    [HttpGet]
    public IEnumerable<BoardDto> Get([FromServices] GetAllBoardsUseCase useCase)
    {
        return useCase.Execute();
    }

    [HttpGet("{slug}")]
    public IActionResult Get([FromRoute] String slug, [FromServices] GetBoardBySlugUseCase useCase)
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
}
