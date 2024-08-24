using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentGatewayService.Models.Entities;

namespace PaymentGatewayService.Data
{
    public class SeedData : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.HasData(new Setting
            {
                Id = 1,
                MerchantId = "1818",
                TerminalId = "53C20VIY",
                TerminalKey = "NzMyMGMyZjA0MDFjYjg2NWU4OGVhMWUy",
                PaymentRequestUrl = "api/v0/Request/PaymentRequest",
                VerifyPaymentUrl = "api/v0/Advice/Verify",
                PurchaseUrl = "Purchase",
                PurchasePageUrl = "https://sandbox.banktest.ir/melli/sadad.shaparak.ir/VPG",
                GatewayName = "Sadad",
                CreatedDate = DateTime.Now
            });
        }
    }
}
