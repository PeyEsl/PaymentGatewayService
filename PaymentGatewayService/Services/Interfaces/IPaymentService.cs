using PaymentGatewayService.Models.Entities;

namespace PaymentGatewayService.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> AddPaymentAsync(Payment payment);
        Task<bool> DeletePaymentAsync(int id);
        Task<bool> ExistsPaymentAsync(int id);
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task<Payment?> GetPaymentByIdAsync(int id);
        Task<IEnumerable<Payment>> SearchPaymentsAsync(string searchTerm);
        Task<bool> UpdatePaymentAsync(Payment payment);
    }
}
