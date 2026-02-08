using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TableConfiguration : IEntityTypeConfiguration<Table>
{
    public void Configure(EntityTypeBuilder<Table> builder)
    {
        builder.ToTable("Tables");
        builder.HasKey(g => g.Id);
        builder.Property(a => a.IsAvailable).HasDefaultValueSql("true");
        builder.Property(a=>a.CreatedAt).HasDefaultValueSql("NOW()");
    }
}