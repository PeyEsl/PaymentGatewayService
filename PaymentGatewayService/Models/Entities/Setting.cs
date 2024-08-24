namespace PaymentGatewayService.Models.Entities
{
    public class Setting
    {
        public int Id { get; set; }
        public string? MerchantId { get; set; }
        public string? TerminalId { get; set; }
        public string? TerminalKey { get; set; }
        public string? PaymentRequestUrl { get; set; }
        public string? VerifyPaymentUrl { get; set; }
        public string? PurchaseUrl { get; set; }
        public string? PurchasePageUrl { get; set; }
        public string? GatewayName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
