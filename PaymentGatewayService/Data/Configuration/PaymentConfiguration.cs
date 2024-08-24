using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentGatewayService.Models.Entities;

namespace PaymentGatewayService.Data.Configuration
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Amount)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0m);

            builder.Property(p => p.PaymentMethod)
                   .HasMaxLength(15);

            builder.Property(p => p.TransactionId)
                   .HasMaxLength(100);

            builder.Property(p => p.SourceCardNumber)
                   .HasMaxLength(20);

            builder.Property(p => p.DestinationCardNumber)
                   .HasMaxLength(20);

            builder.Property(p => p.DestinationAccountNumber)
                   .HasMaxLength(20);

            builder.Property(p => p.PaymentGatewayTrackingCode)
                   .HasMaxLength(50);

            builder.Property(p => p.UserIpAddress)
                   .HasMaxLength(20);

            builder.Property(p => p.CreatedDate)
                   .HasDefaultValueSql("GETDATE()")
                   .ValueGeneratedOnAdd();

            builder.Property(p => p.UpdatedDate)
                   .IsRequired(false);

            builder.Property(p => p.OrderId)
                   .HasMaxLength(15);
            
            builder.Property(p => p.UserId)
                   .HasMaxLength(50);
        }
    }
}
