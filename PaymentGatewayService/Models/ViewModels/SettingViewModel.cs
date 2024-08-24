using System.ComponentModel.DataAnnotations;

namespace PaymentGatewayService.Models.ViewModels
{
    public class SettingViewModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "شماره پذيرنده:*")]
        [Required(ErrorMessage = "لطفاً شماره پذيرنده را وارد کنید.")]
        public string? MerchantId { get; set; }

        [Display(Name = "شماره ترمینال:*")]
        [Required(ErrorMessage = "لطفاً شماره ترمینال را وارد کنید.")]
        public string? TerminalId { get; set; }

        [Display(Name = "کلید تراکنش:*")]
        public string? TerminalKey { get; set; }

        [Display(Name = "آدرس درخواست پرداخت:*")]
        public string? PaymentRequestUrl { get; set; }

        [Display(Name = "آدرس تأیید پرداخت:*")]
        public string? VerifyPaymentUrl { get; set; }

        [Display(Name = "آدرس خرید اینترنتی:*")]
        public string? PurchaseUrl { get; set; }
        
        [Display(Name = "آدرس صفحه خرید اینترنتی:*")]
        public string? PurchasePageUrl { get; set; }

        [Display(Name = "نام درگاه:*")]
        public string? GatewayName { get; set; }

        [Display(Name = "تاریخ ایجاد:")]
        public string? CreatedDate { get; set; }
    }
}
