using boards.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace boards.Infrastructure.Context;

public sealed class BoardDbContextConfiguration : IEntityTypeConfiguration<BoardDb>
{
    public void Configure(EntityTypeBuilder<BoardDb> builder)
    {
        builder.HasKey(x => x.Slug);
    }
}