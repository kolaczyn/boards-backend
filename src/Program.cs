using boards.Application;
using boards.Domain;
using boards.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<GetAllBoardsUseCase>();
builder.Services.AddTransient<IBoardsRepository, BoardsRepository>();
builder.Services.AddTransient<GetBoardBySlugUseCase>();
builder.Services.AddTransient<GetAllBoardsUseCase>();
builder.Services.AddTransient<CreateBoardUseCase>();

builder.Services.AddDbContext<BoardDbContext>(options =>
{
    options.UseSqlite("Data Source=boards.db");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();