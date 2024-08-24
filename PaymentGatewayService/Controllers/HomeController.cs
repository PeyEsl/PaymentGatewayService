using Microsoft.AspNetCore.Mvc;
using NuGet.Configuration;
using PaymentGatewayService.Models;
using PaymentGatewayService.Models.Entities;
using PaymentGatewayService.Models.ViewModels;
using PaymentGatewayService.Services.Interfaces;
using System.Diagnostics;

namespace PaymentGatewayService.Controllers
{
    public class HomeController : Controller
    {
        #region Ctor

        private readonly ILogger<HomeController> _logger;
        private readonly ISettingService _settingService;
        private readonly IPaymentService _paymentService;
        private readonly ISadadPaymentService _sadadPaymentService;

        public HomeController(
            ILogger<HomeController> logger,
            ISettingService settingService,
            IPaymentService paymentService,
            ISadadPaymentService sadadPaymentService)
        {
            _logger = logger;
            _settingService = settingService;
            _paymentService = paymentService;
            _sadadPaymentService = sadadPaymentService;
        }

        #endregion

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<ActionResult> PaymentList()
        {
            try
            {
                var payments = await _paymentService.GetAllPaymentsAsync();
                if (payments == null)
                {
                    ViewData["ErrorMessage"] = "لیست پرداخت خالی است.";

                    return View(new List<PaymentViewModel>());
                }

                var paymentList = new List<PaymentViewModel>();
                foreach (var payment in payments)
                {
                    paymentList.Add(new PaymentViewModel
                    {
                        Id = payment.Id,
                        Amount = payment.Amount,
                        PaymentMethod = payment.PaymentMethod,
                        TransactionId = payment.TransactionId,
                        SourceCardNumber = string.Empty,
                        DestinationCardNumber = string.Empty,
                        DestinationAccountNumber = string.Empty,
                        PaymentGatewayTrackingCode = payment.PaymentGatewayTrackingCode,
                        UserIpAddress = payment.UserIpAddress,
                        CreatedDate = payment.CreatedDate,
                        UpdatedDate = null,
                        OrderId = payment.OrderId.ToString(),
                        UserId = payment.UserId,
                    });
                }

                return View(paymentList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "هنگام واکشی پرداخت‌ها خطایی رخ داد.");
                ViewData["ErrorMessage"] = "هنگام واکشی پرداخت‌ها خطایی رخ داد. لطفاً بعداً دوباره امتحان کنید.";
                return View(new List<PaymentViewModel>());
            }
        }

        public async Task<IActionResult> SadadPayment()
        {
            try
            {
                var settings = await _settingService.GetAllSettingsAsync();
                if (settings == null)
                {
                    ViewData["ErrorMessage"] = "لیست تنظیمات خالی است.";

                    return View(new PaymentRequestViewModel());
                }

                var setting = settings.LastOrDefault();
                if (setting == null)
                {
                    ViewData["ErrorMessage"] = "لیست تنظیمات خالی است.";

                    return View(new PaymentRequestViewModel());
                }

                var model = new PaymentRequestViewModel
                {
                    TerminalId = setting.TerminalId,
                    MerchantId = setting.MerchantId,
                    Amount = new Random().Next(1000000),
                    MerchantKey = setting.TerminalKey,
                    PurchasePage = setting.PurchasePageUrl,
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "هنگام واکشی پرداخت اینترنتی خطایی رخ داد.");
                ViewData["ErrorMessage"] = "هنگام واکشی پرداخت اینترنتی خطایی رخ داد. لطفاً بعداً دوباره امتحان کنید.";
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> SadadPayment(PaymentRequestViewModel request)
        {
            if (!ModelState.IsValid)
                return View(request);

            try
            {
                request.OrderId = new Random().Next(1000);
                request.LocalDateTime = DateTime.Now;

                var res = await _sadadPaymentService.PaymentRequest(request);
                if (res == null)
                {
                    ViewData["ErrorMessage"] = "درخواست پرداخت ناموفق بود.";

                    return View();
                }

                if (res.ResCode == 0)
                {
                    Response.Redirect(string.Format("{0}/Purchase?token={1}", request.PurchasePage, res.Token));
                }

                ViewData["Message"] = res.Description;

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "هنگام ایجاد پرداخت اینترنتی خطایی رخ داد.");
                ViewData["ErrorMessage"] = "هنگام ایجاد پرداخت اینترنتی خطایی رخ داد. لطفاً بعداً دوباره امتحان کنید.";
                return View();
            }
        }

        [HttpPost]
        public ActionResult VerifyPayment(PurchaseResultViewModel result)
        {
            return View(result);
        }

        [HttpPost]
        public async Task<ActionResult> VerifyRequest(PurchaseResultViewModel result)
        {
            try
            {
                var res = await _sadadPaymentService.VerifyPayment(result);
                if (res == null)
                {
                    ViewData["ErrorMessage"] = "تأیید پرداخت ناموفق بود.";

                    return View(result);
                }

                if (res.ResCode == 0)
                {
                    result.VerifyResultData = res;
                    res.Succeed = true;

                    ViewData["Message"] = res.Description;

                    return View("VerifyPayment", result);
                }

                ViewData["Message"] = res.Description;

                return View(nameof(VerifyPayment));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "هنگام تأیید پرداخت اینترنتی خطایی رخ داد.");
                ViewData["ErrorMessage"] = "هنگام تأیید پرداخت اینترنتی خطایی رخ داد. لطفاً بعداً دوباره امتحان کنید.";
                return View("SadadPayment");
            }
        }

        [HttpPost]
        public async Task<ActionResult> RegisteringInformation(PurchaseResultViewModel result)
        {
            try
            {
                var res = await _sadadPaymentService.VerifyPayment(result);
                if (res == null)
                {
                    ViewData["ErrorMessage"] = "تأیید پرداخت ناموفق بود.";

                    return View(result);
                }

                if (res.ResCode == 0)
                {
                    var payment = new Payment
                    {
                        Amount = res.Amount,
                        PaymentMethod = "پرداخت اینترنتی",
                        TransactionId = res.RetrivalRefNo,
                        PaymentGatewayTrackingCode = res.SystemTraceNo,
                        UserIpAddress = GetUserIpAddress(),
                        CreatedDate = DateTime.Now,
                        OrderId = result.OrderId,
                        UserId = Guid.NewGuid().ToString(),
                    };

                    var resultPayment = await _paymentService.AddPaymentAsync(payment);
                    if (resultPayment)
                    {
                        ViewData["Message"] = "پرداخت با موفقیت ثبت شد.";

                        return View("Index");
                    }
                }

                ViewData["Message"] = res.Description;

                return View(nameof(VerifyPayment));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "هنگام ایجاد پرداخت خطایی رخ داد.");
                ViewData["ErrorMessage"] = "هنگام ایجاد پرداخت خطایی رخ داد. لطفاً بعداً دوباره امتحان کنید.";
                return View();
            }
        }

        private string GetUserIpAddress()
        {
            // بررسی هدر "X-Forwarded-For" در صورت استفاده از پروکسی
            var ipAddress = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ipAddress))
            {
                // استفاده از RemoteIpAddress در صورت نبودن هدر X-Forwarded-For
                ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            }

            if (ipAddress == "::1")
            {
                ipAddress = "127.0.0.1"; // برای لوکال‌هاست به IPv4 تبدیل کنید
            }

            return ipAddress ?? "IP Not Found";
        }
    }
}
