namespace PaymentGatewayService.Models.ViewModels
{
    public class VerifyResultDataViewModel
    {
        public bool Succeed { get; set; }
        public long ResCode { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public string? RetrivalRefNo { get; set; }
        public string? SystemTraceNo { get; set; }
        public long OrderId { get; set; }
    }
}
