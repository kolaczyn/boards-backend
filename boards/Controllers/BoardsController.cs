using boards.Dto;
using boards.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace boards.Controllers;

[ApiController]
[Route("[controller]")]
public class BoardsController : ControllerBase
{
    [HttpGet]
    public IEnumerable<BoardDto> Get()
    {
        var useCase = new GetAllBoardsUseCase();
        return useCase.Execute();
    }
}