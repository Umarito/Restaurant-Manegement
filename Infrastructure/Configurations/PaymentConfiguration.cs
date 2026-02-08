using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");
        builder.HasKey(g => g.Id);
        builder.Property(a => a.OrderId).IsRequired();
        builder.Property(a=>a.CreatedAt).HasDefaultValueSql("NOW()");
    }
}