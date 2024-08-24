using PaymentGatewayService.Models.Entities;

namespace PaymentGatewayService.Services.Interfaces
{
    public interface ISettingService
    {
        Task<bool> AddSettingAsync(Setting setting);
        Task<bool> DeleteSettingAsync(int id);
        Task<bool> ExistsSettingAsync(int id);
        Task<IEnumerable<Setting>> GetAllSettingsAsync();
        Task<Setting?> GetSettingByIdAsync(int id);
        Task<IEnumerable<Setting>> SearchSettingsAsync(string searchTerm);
        Task<bool> UpdateSettingAsync(Setting setting);
    }
}
