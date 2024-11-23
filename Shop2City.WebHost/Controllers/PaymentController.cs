
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Shop2City.Core.Services.Factors;
using Shop2City.Core.Services.Orders;
using Shop2City.Core.Services.Transactions;
using Shop2City.Core.Services.Users;
using Shop2City.DataLayer.Entities;
using Shop2City.DataLayer.Entities.Advices;
using Shop2City.DataLayer.Entities.Transactions;
using System.Text;
using System.Text.Json;


namespace Shop2City.WebHost.Controllers
{
    public class PaymentController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IUserService _userService;
        private readonly ITransactionService _transactionService;
        private readonly IFactorService _factorService;
        private readonly IOrderService _orderService;

        public PaymentController(IUserService userService, ITransactionService transactionService, IFactorService factorService, IOrderService orderService)
        {
            _userService = userService;
            _transactionService = transactionService;
            _factorService = factorService;
            _orderService = orderService;
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(300)
            };
            _factorService = factorService;
        }
        [HttpPost]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequest request)
        {
            string callbackUrl = $"https://madwin.ir/Payment/Verify?source={request.Source}";
            string url = "https://sepehr.shaparak.ir:8081/V1/PeymentApi/GetToken";
            var paymentRequest = new
            {
                TerminalID = "98808771",
                Amount = request.SumOrder, // اینجا می‌توانید از request.SumOrder استفاده کنید
                InvoiceID = request.InvoiceId, // در صورت نیاز، مقدار فاکتور را از جدول بگیرید
                callbackURL = callbackUrl,
                payload = ""
            };

            // سریالایز کردن داده‌های درخواست پرداخت
            string jsonData = JsonSerializer.Serialize(paymentRequest);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // ارسال درخواست برای دریافت توکن
            var response = await _httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(responseData);
                var token = jsonObject["Accesstoken"]?.ToString();

                if (!string.IsNullOrEmpty(token))
                {
                    // ساخت آدرس بازگشتی به همراه توکن و سایر پارامترها
                    string redirectUrl = $"https://sepehr.shaparak.ir:8080/Pay?token={token}&terminalID=98808771&getMethod=0";
                    return Json(new { success = true, redirectUrl = redirectUrl });
                }

                return Json(new { success = false, message = "توکن از درگاه پرداخت دریافت نشد." });
            }
            else
            {
                return Json(new { success = false, message = "خطا در دریافت توکن پرداخت." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Verify()
        {
            if (!Request.HasFormContentType)
            {
                return BadRequest("داده‌های فرم نادرست دریافت شده است.");
            }
            var source = Request.Query["source"].ToString();
            var amountStr = Request.Form["Amount"];
            var digitalReceipt = Request.Form["DigitalReceipt"].ToString();
            var datePaidStr = Request.Form["DatePaid"].ToString();
            var terminalId = Request.Form["TerminalId"].ToString();
            int invoiceId = int.Parse(Request.Form["InvoiceId"]);
            var cardNumber = Request.Form["CardNumber"].ToString();
            var payload = Request.Form["Payload"].ToString();
            var rrn = Request.Form["Rrn"].ToString();
            var respMsg = Request.Form["RespMsg"].ToString();
            var traceNumber = Request.Form["TraceNumber"].ToString();
            var respCode = Request.Form["RespCode"].ToString();
            var issuerBank = Request.Form["IssuerBank"].ToString();
            // ارسال درخواست Advice
            var url = "https://sepehr.shaparak.ir:8081/V1/PeymentApi/Advice";
            var advice = new
            {
                digitalreceipt = digitalReceipt,
                Tid = terminalId
            };

            var jsonData = JsonSerializer.Serialize(advice);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);  // عملیات غیرهمزمان برای ارسال درخواست
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(responseData);
                var adviceModel = new AdviceModel
                {
                    Status = jsonObject["Status"]?.ToString(),
                    ReturnId = jsonObject["ReturnId"]?.ToString(),
                    Message = jsonObject["Message"]?.ToString(),
                };
                await _transactionService.AddAdvice(adviceModel);

                var transactionModel = new TransactionModel
                {
                    DigitalReceipt=digitalReceipt,
                    Amount=amountStr,
                    CardNumber=cardNumber,
                    DatePaid=datePaidStr,
                    InvoiceId=invoiceId,
                    IssuerBank=issuerBank,
                    Payload=payload,
                    RespCode=respCode,
                    RespMsg=respMsg,
                    Rrn=rrn,
                    TerminalId=terminalId,
                    TraceNumber=traceNumber
                };
                await _transactionService.AddTransaction(transactionModel);
            }

            if (source == "order")
            {
                return RedirectToAction("ShowOrderForUser", "Orders", new { area = "UserPanel", orderId = invoiceId });
            }
            else if (source == "factor")
            {
                return RedirectToAction("ShowFactorForUser", "Factors", new { area = "UserPanel", factorId = invoiceId });
            }

            return BadRequest("پارامتر source نامعتبر است.");
        }

        public class PaymentRequest
        {
            public int SumOrder { get; set; }
            public int InvoiceId { get; set; }
            public string Source { get; set; }
        }
    }
}
                                                                                                                                                                                        