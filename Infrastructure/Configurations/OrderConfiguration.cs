using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        builder.HasKey(g => g.Id);
        builder.Property(a=>a.CreatedAt).HasDefaultValueSql("NOW()");
        builder.HasOne(a => a.Waiter).WithMany(a => a.OrdersAsWaiter).HasForeignKey(a => a.WaiterId);
    }
}