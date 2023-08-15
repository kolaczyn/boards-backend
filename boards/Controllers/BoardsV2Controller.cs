using boards.Application.Dto;
using boards.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace boards.Controllers;

[ApiController]
[Route("v2/boards")]
public class BoardsV2Controller : ControllerBase
{
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoriesBoardsDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBoards([FromServices] GetAllBoardsV2UseCase useCase,
        CancellationToken cancellationToken) =>
        Ok(await useCase.Execute(cancellationToken));
    
}