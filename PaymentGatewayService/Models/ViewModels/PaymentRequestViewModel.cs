using System.ComponentModel.DataAnnotations;

namespace PaymentGatewayService.Models.ViewModels
{
    public class PaymentRequestViewModel
    {
        [Display(Name = @"شماره ترمینال")]
        [Required(ErrorMessage = "شماره پایانه اجباری است ")]
        public string? TerminalId { get; set; }

        [Display(Name = @"شماره پذیرنده")]
        [Required(ErrorMessage = "شماره پذیرنده اجباری است ")]
        public string? MerchantId { get; set; }

        [Required(ErrorMessage = "مبلغ اجباری است ")]
        [Display(Name = @"مبلغ")]
        public decimal Amount { get; set; }
        public long OrderId { get; set; }
        public string? AdditionalData { get; set; }
        public DateTime LocalDateTime { get; set; }
        public string? ReturnUrl { get; set; }
        public string? SignData { get; set; }
        [Display(Name = @"پرداخت تسهیم")]
        public bool EnableMultiplexing { get; set; }
        public MultiplexingDataViewModel? MultiplexingData { get; set; }

        [Display(Name = @"کلید پذیرنده")]
        [Required(ErrorMessage = "کلیدپذیرنده اجباری است ")]
        public string? MerchantKey { get; set; }

        [Display(Name = @"آدرس درگاه")]
        [Required(ErrorMessage = "آدرس درگاه اجباری است ")]
        public string? PurchasePage { get; set; }
    }
}
