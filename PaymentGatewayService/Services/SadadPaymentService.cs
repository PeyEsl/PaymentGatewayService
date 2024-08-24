using PaymentGatewayService.Models.Entities;
using PaymentGatewayService.Models.ViewModels;
using PaymentGatewayService.Services.Interfaces;
using PaymentGatewayService.Tools;
using System.Net;

namespace PaymentGatewayService.Services
{
    public class SadadPaymentService : ISadadPaymentService
    {
        #region Ctor

        private readonly ILogger<SadadPaymentService> _logger;
        private readonly ISettingService _settingService;
        private readonly IPaymentService _paymentService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SadadPaymentService(
            ILogger<SadadPaymentService> logger,
            ISettingService settingService,
            IPaymentService paymentService,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _settingService = settingService;
            _paymentService = paymentService;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        public async Task<PayResultDataViewModel> PaymentRequest(PaymentRequestViewModel request)
        {
            try
            {
                var setting = await GetPaymentGatewaySettings();
                if (setting == null)
                {
                    return new PayResultDataViewModel();
                }

                var plainText = $"{setting.TerminalId};{request.OrderId};{request.Amount}";
                var signData = EncryptionHelper.EncryptTripleDES(plainText, setting.TerminalKey!);
                var requestData = new PaymentRequestViewModel
                {
                    MerchantId = setting.MerchantId,
                    TerminalId = setting.TerminalId,
                    Amount = (long)request.Amount,
                    LocalDateTime = DateTime.Now,
                    SignData = signData,
                    OrderId = request.OrderId,
                    ReturnUrl = string.Format("{0}://{1}/{2}Home/VerifyRequest", 
                                _httpContextAccessor.HttpContext!.Request.Scheme,
                                _httpContextAccessor.HttpContext!.Request.Host,
                                _httpContextAccessor.HttpContext.Request.PathBase),
                };

                var ipgUri = string.Format("{0}/{1}", request.PurchasePage, setting.PaymentRequestUrl);
                return await CallApi<PayResultDataViewModel>(ipgUri!, requestData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while requesting the payment.");
                throw new Exception("Payment request failed.", ex);
            }
        }

        public async Task<VerifyResultDataViewModel> VerifyPayment(PurchaseResultViewModel result)
        {
            var setting = await GetPaymentGatewaySettings();
            if (setting == null)
            {
                return new VerifyResultDataViewModel();
            }

            var signData = EncryptionHelper.EncryptTripleDES(result.Token!, setting.TerminalKey!);
            var data = new
            {
                token = result.Token,
                SignData = signData,
            };

            var ipgUri = string.Format("{0}/{1}", setting.PurchasePageUrl, setting.VerifyPaymentUrl);

            return await CallApi<VerifyResultDataViewModel>(ipgUri!, data);
        }

        private async Task<Setting> GetPaymentGatewaySettings()
        {
            var settings = await _settingService.GetAllSettingsAsync();
            if (settings == null)
            {
                return new Setting();
            }

            var setting = settings.LastOrDefault();
            if (setting == null)
            {
                return new Setting();
            }

            return setting;
        }

        public static async Task<T> CallApi<T>(string apiUrl, object value)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();

                HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, value);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<T>();
                    return result!;
                }

                return default(T)!;
            }
        }
    }
}
