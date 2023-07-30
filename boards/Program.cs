using boards.Application.UseCases;
using boards.Domain;
using boards.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddTransient<GetAllBoardsUseCase>();
    builder.Services.AddTransient<GetBoardBySlugUseCase>();
    builder.Services.AddTransient<GetAllBoardsUseCase>();
    builder.Services.AddTransient<CreateBoardUseCase>();
    builder.Services.AddTransient<GetThreadsListUseCase>();
    builder.Services.AddTransient<CreateThreadUseCase>();
    builder.Services.AddTransient<CreateReplyUseCase>();
    builder.Services.AddTransient<GetThreadUseCase>();
        
    builder.Services.AddTransient<IBoardsRepository, BoardsRepository>();
}

builder.Services.AddDbContext<BoardDbContext>(options => { options.UseSqlite("Data Source=boards.db"); });

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