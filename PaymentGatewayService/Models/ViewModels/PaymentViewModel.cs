using System.ComponentModel.DataAnnotations;

namespace PaymentGatewayService.Models.ViewModels
{
    public class PaymentViewModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "مبلغ پرداختی:")]
        [Required(ErrorMessage = "لطفاً مبلغ پرداختی را وارد کنید.")]
        public decimal Amount { get; set; }

        [Display(Name = "نوع پرداخت:")]
        [Required(ErrorMessage = "لطفاً نوع پرداخت را مشخص کنید.")]
        public string? PaymentMethod { get; set; }

        [Display(Name = "شناسه تراکنش:")]
        [Required(ErrorMessage = "لطفاً شناسه تراکنش را وارد کنید.")]
        public string? TransactionId { get; set; }

        [Display(Name = "شماره کارت مبدا:")]
        public string? SourceCardNumber { get; set; }

        [Display(Name = "شماره کارت مقصد:")]
        public string? DestinationCardNumber { get; set; }

        [Display(Name = "شماره حساب مقصد:")]
        public string? DestinationAccountNumber { get; set; }

        [Display(Name = "کد رهگیری:")]
        public string? PaymentGatewayTrackingCode { get; set; }

        [Display(Name = "IP آدرس کاربر:")]
        [Required(ErrorMessage = "لطفاً IP آدرس کاربر را وارد کنید.")]
        public string? UserIpAddress { get; set; }

        [Display(Name = "تاریخ ایجاد:")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "تاریخ بروزرسانی:")]
        public DateTime? UpdatedDate { get; set; }
        public string? OrderId { get; set; }
        public string? UserId { get; set; }
    }
}
