using PaymentGatewayService.Models.ViewModels;

namespace PaymentGatewayService.Services.Interfaces
{
    public interface ISadadPaymentService
    {
        Task<PayResultDataViewModel> PaymentRequest(PaymentRequestViewModel request);
        Task<VerifyResultDataViewModel> VerifyPayment(PurchaseResultViewModel result);
    }
}
