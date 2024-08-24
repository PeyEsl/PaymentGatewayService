using Microsoft.EntityFrameworkCore;
using PaymentGatewayService.Services.Interfaces;
using PaymentGatewayService.Models.Entities;
using PaymentGatewayService.Data;

namespace PaymentGatewayService.Services
{
    public class PaymentService : IPaymentService
    {
        #region Ctor

        private readonly ILogger<PaymentService> _logger;
        private readonly ApplicationDbContext _context;

        public PaymentService(ILogger<PaymentService> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        #endregion

        public async Task<bool> AddPaymentAsync(Payment payment)
        {
            try
            {
                await _context.Set<Payment>()
                              .AddAsync(payment);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the payment.");
                throw new Exception("Error adding payment.", ex);
            }
        }

        public async Task<bool> DeletePaymentAsync(int id)
        {
            try
            {
                var payment = await GetPaymentByIdAsync(id);
                if (payment != null)
                {
                    _context.Set<Payment>()
                            .Remove(payment);

                    await _context.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the payment.");
                throw new Exception("Error deleting payment.", ex);
            }
        }

        public async Task<bool> ExistsPaymentAsync(int id)
        {
            try
            {
                return await _context.Set<Setting>()
                                     .AsNoTracking()
                                     .AnyAsync(p => p.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while existing the payment.");
                throw new Exception("Error existing payment.", ex);
            }
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            try
            {
                return await _context.Set<Payment>()
                                     .AsNoTracking()
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while reading the payments.");
                throw new Exception("Error retrieving payments.", ex);
            }
        }

        public async Task<Payment?> GetPaymentByIdAsync(int id)
        {
            try
            {
                return await _context.Set<Payment>()
                                     .FirstOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while reading the payment.");
                throw new Exception("Error retrieving payment.", ex);
            }
        }

        public async Task<IEnumerable<Payment>> SearchPaymentsAsync(string searchTerm)
        {
            try
            {
                return await _context.Set<Payment>()
                                     .AsNoTracking()
                                     .Where(p => p.PaymentMethod!.Contains(searchTerm) ||
                                                 p.TransactionId!.Contains(searchTerm))
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while searching the payments with term: {searchTerm}");
                throw new Exception($"Error searching payments with term: {searchTerm}.", ex);
            }
        }

        public async Task<bool> UpdatePaymentAsync(Payment payment)
        {
            try
            {
                var existingPayment = await _context.Set<Payment>()
                                                    .FirstOrDefaultAsync(p => p.Id == payment.Id);
                if (existingPayment == null)
                                       throw new Exception("Payment not found.");

                existingPayment.Amount = payment.Amount;
                existingPayment.PaymentMethod = payment.PaymentMethod;
                existingPayment.TransactionId = payment.TransactionId;
                existingPayment.SourceCardNumber = payment.SourceCardNumber;
                existingPayment.DestinationCardNumber = payment.DestinationCardNumber;
                existingPayment.DestinationAccountNumber = payment.DestinationAccountNumber;
                existingPayment.PaymentGatewayTrackingCode = payment.PaymentGatewayTrackingCode;
                existingPayment.UserIpAddress = payment.UserIpAddress;
                existingPayment.UpdatedDate = DateTime.Now;
                existingPayment.OrderId = payment.OrderId;
                existingPayment.UserId = payment.UserId;

                _context.Set<Payment>()
                        .Update(existingPayment);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the payment.");
                throw new Exception("Error updating payment.", ex);
            }
        }
    }
}
