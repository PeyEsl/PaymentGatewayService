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
                MerchantId = "YOUR_MERCHANT_ID",
                TerminalId = "YOUR_TERMINAL_ID",
                TerminalKey = "YOUR_MERCHANT_KEY",
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
