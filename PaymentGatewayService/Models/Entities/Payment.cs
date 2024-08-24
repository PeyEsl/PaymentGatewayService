namespace PaymentGatewayService.Models.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string? PaymentMethod { get; set; }
        public string? TransactionId { get; set; }
        public string? SourceCardNumber { get; set; }
        public string? DestinationCardNumber { get; set; }
        public string? DestinationAccountNumber { get; set; }
        public string? PaymentGatewayTrackingCode { get; set; }
        public string? UserIpAddress { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long OrderId { get; set; }
        public string? UserId { get; set; }
    }
}
