using Microsoft.EntityFrameworkCore;
using PaymentGatewayService.Services.Interfaces;
using PaymentGatewayService.Models.Entities;
using PaymentGatewayService.Data;

namespace PaymentGatewayService.Services
{
    public class SettingService : ISettingService
    {
        #region Ctor

        private readonly ILogger<SettingService> _logger;
        private readonly ApplicationDbContext _context;

        public SettingService(ILogger<SettingService> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        #endregion

        public async Task<bool> AddSettingAsync(Setting setting)
        {
            try
            {
                await _context.Set<Setting>()
                              .AddAsync(setting);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the  setting.");
                throw new Exception("Error adding setting.", ex);
            }
        }

        public async Task<bool> DeleteSettingAsync(int id)
        {
            try
            {
                var setting = await GetSettingByIdAsync(id);
                if (setting != null)
                {
                    _context.Set<Setting>()
                            .Remove(setting);

                    await _context.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the setting.");
                throw new Exception("Error deleting setting.", ex);
            }
        }

        public async Task<bool> ExistsSettingAsync(int id)
        {
            try
            {
                return await _context.Set<Setting>()
                                     .AsNoTracking()
                                     .AnyAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while existing the setting.");
                throw new Exception("Error existing setting.", ex);
            }
        }

        public async Task<IEnumerable<Setting>> GetAllSettingsAsync()
        {
            try
            {
                return await _context.Set<Setting>()
                                     .AsNoTracking()
                                     .Where(s => s.GatewayName == "Sadad")
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while reading the settings.");
                throw new Exception("Error retrieving settings.", ex);
            }
        }

        public async Task<Setting?> GetSettingByIdAsync(int id)
        {
            try
            {
                return await _context.Set<Setting>()
                                     .Where(s => s.GatewayName == "Sadad")
                                     .FirstOrDefaultAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while reading the setting.");
                throw new Exception("Error retrieving setting.", ex);
            }
        }

        public async Task<IEnumerable<Setting>> SearchSettingsAsync(string searchTerm)
        {
            try
            {
                return await _context.Set<Setting>()
                                     .AsNoTracking()
                                     .Where(s => s.MerchantId!.Contains(searchTerm) ||
                                                 s.TerminalId!.Contains(searchTerm))
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while searching the settings with term: {searchTerm}");
                throw new Exception($"Error searching settings with term: {searchTerm}.", ex);
            }
        }

        public async Task<bool> UpdateSettingAsync(Setting setting)
        {
            try
            {
                var existingSetting = await _context.Set<Setting>()
                                                    .Where(s => s.GatewayName == "Sadad")
                                                    .FirstOrDefaultAsync(s => s.Id == setting.Id);
                if (existingSetting == null)
                                       throw new Exception("Setting not found.");

                existingSetting.MerchantId = setting.MerchantId;
                existingSetting.TerminalId = setting.TerminalId;
                existingSetting.TerminalKey = setting.TerminalKey;
                existingSetting.PaymentRequestUrl = setting.PaymentRequestUrl;
                existingSetting.VerifyPaymentUrl = setting.VerifyPaymentUrl;
                existingSetting.PurchaseUrl = setting.PurchaseUrl;

                _context.Set<Setting>()
                        .Update(existingSetting);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the setting.");
                throw new Exception("Error updating setting.", ex);
            }
        }
    }
}
