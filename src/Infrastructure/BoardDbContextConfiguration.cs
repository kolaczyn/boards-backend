using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace boards.Infrastructure;

public sealed class BoardDbContextConfiguration : IEntityTypeConfiguration<BoardDb>
{
    public void Configure(EntityTypeBuilder<BoardDb> builder)
    {
        builder.HasKey(x => x.Slug);
    }
}