using boards.Application;
using boards.Dto;
using Microsoft.AspNetCore.Mvc;

namespace boards.Controllers;

[ApiController]
[Route("[controller]")]
public class BoardsController : ControllerBase
{
    [HttpGet]
    public IEnumerable<BoardDto> Get(GetAllBoardsUseCase useCase)
    {
        return useCase.Execute();
    }
}