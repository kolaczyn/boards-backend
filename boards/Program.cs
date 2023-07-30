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

builder.Services.AddDbContext<BoardDbContext>(options =>
{
    options.UseSqlite("Data Source=boards.db");
    
});

var app = builder.Build();

app.Use(async (ctx, next) =>
{
    ctx.Response.Headers["Access-Control-Allow-Origin"] = "*";
    ctx.Response.Headers["Access-Control-Allow-Headers"] = "Origin, X-Requested-With, Content-Type, Accept";
    ctx.Response.Headers["Access-Control-Allow-Methods"] = "GET, POST, PUT, DELETE, OPTIONS";

    if (HttpMethods.IsOptions(ctx.Request.Method))
    {
        ctx.Response.Headers["Access-Control-Allow-Methods"] = "GET, POST, PUT, DELETE, OPTIONS";
        await ctx.Response.CompleteAsync();
        return;
    }
    await  next();
});

{
    using var scope = ServiceProviderServiceExtensions.CreateScope(app.Services);
    var dbContext = scope.ServiceProvider.GetRequiredService<BoardDbContext>();
    await dbContext.Database.MigrateAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();