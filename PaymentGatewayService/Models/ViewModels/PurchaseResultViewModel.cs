namespace PaymentGatewayService.Models.ViewModels
{
    public class PurchaseResultViewModel
    {
        public long OrderId { get; set; }
        public string? Token { get; set; }
        public string? ResCode { get; set; }
        public VerifyResultDataViewModel? VerifyResultData { get; set; }
    }
}
