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
    
    [HttpGet("{id}")]
    public IActionResult Get( [FromRoute] String id,[FromServices] GetBoardBySlugUseCase useCase)
    {
        var result = useCase.Execute(id);
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }
}