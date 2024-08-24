namespace PaymentGatewayService.Models.ViewModels
{
    public class PayResultDataViewModel
    {
        public long ResCode { get; set; }
        public string? Token { get; set; }
        public string? Description { get; set; }
    }
}
