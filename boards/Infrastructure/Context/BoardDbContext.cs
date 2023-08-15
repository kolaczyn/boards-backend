using boards.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace boards.Infrastructure.Context;

public class BoardDbContext : DbContext
{
    public BoardDbContext(DbContextOptions<BoardDbContext> options) : base(options) { }
    
    public DbSet<CategoryDb> Categories { get; set; } = null!;
    public DbSet<BoardDb> Boards { get; set; } = null!;
    public DbSet<ThreadDb> Threads { get; set; } = null!;
    public DbSet<ReplyDb> Replies { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BoardDbContext).Assembly);
    }
}