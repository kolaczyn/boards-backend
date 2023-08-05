using System.Text.Json.Serialization;
using boards.Application.UseCases;
using boards.Domain.Providers;
using boards.Domain.Repositories;
using boards.Infrastructure;
using boards.Infrastructure.Context;
using boards.Infrastructure.Providers;
using boards.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

{
    builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddTransient<GetAllBoardsUseCase>();
    builder.Services.AddTransient<GetAllBoardsUseCase>();
    builder.Services.AddTransient<CreateBoardUseCase>();
    builder.Services.AddTransient<GetThreadsListUseCase>();
    builder.Services.AddTransient<CreateThreadUseCase>();
    builder.Services.AddTransient<CreateReplyUseCase>();
    builder.Services.AddTransient<GetThreadUseCase>();
    builder.Services.AddTransient<DeleteReplyUseCase>();

    builder.Services.AddTransient<IBoardsRepository, BoardsRepository>();
    builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
    builder.Services.AddTransient<ICheckPassword, CheckPassword>();
    
    builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("Secrets"));
}

builder.Services.AddDbContext<BoardDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
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
    app.UseSwaggerUI(options =>
    {
        options.EnableTryItOutByDefault();
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();