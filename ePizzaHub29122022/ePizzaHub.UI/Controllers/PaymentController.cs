using ePizzaHub.Models;
using ePizzaHub.Services.Interfaces;
using ePizzaHub.UI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ePizzaHub.UI.Controllers
{
    public class PaymentController : Controller
    {
        IConfiguration _configuration;
        IPaymentService _paymentService;
        public PaymentController(IConfiguration configuration, IPaymentService paymentService)
        {
            _configuration = configuration;
            _paymentService = paymentService;
        }
        public IActionResult Index()
        {
            PaymentModel payment = new PaymentModel();
            CartModel cart = TempData.Peek<CartModel>("Cart");
            if (cart != null)
            {
                payment.Cart = cart;
                payment.GrandTotal = Math.Round(cart.GrandTotal);
                payment.Currency = "INR";
                payment.Description = string.Join(",", cart.Items.Select(r => r.Name));
                payment.RazorpayKey = _configuration["Razorpay:Key"];
                payment.Receipt = payment.Description;
            }
            return View(payment);
        }
    }
}
