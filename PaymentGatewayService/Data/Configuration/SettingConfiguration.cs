using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentGatewayService.Models.Entities;

namespace PaymentGatewayService.Data.Configuration
{
    public class SettingConfiguration : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.ToTable("Settings");

            builder.HasKey(s => s.Id);

            builder.Property(pgc => pgc.MerchantId)
                   .HasMaxLength(200);

            builder.Property(pgc => pgc.TerminalId)
                   .HasMaxLength(200);

            builder.Property(pgc => pgc.TerminalKey)
                   .HasMaxLength(500);

            builder.Property(pgc => pgc.PaymentRequestUrl)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(pgc => pgc.VerifyPaymentUrl)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(pgc => pgc.PurchaseUrl)
                   .HasMaxLength(200)
                   .IsRequired();
            
            builder.Property(pgc => pgc.PurchasePageUrl)
                   .HasMaxLength(200)
                   .IsRequired();
            
            builder.Property(pgc => pgc.GatewayName)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(c => c.CreatedDate)
                   .HasDefaultValueSql("GETDATE()")
                   .ValueGeneratedOnAdd();
        }
    }
}
