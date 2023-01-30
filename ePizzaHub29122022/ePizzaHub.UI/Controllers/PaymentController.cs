using ePizzaHub.Core.Entities;
using ePizzaHub.Models;
using ePizzaHub.Services.Interfaces;
using ePizzaHub.UI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;

namespace ePizzaHub.UI.Controllers
{
    public class PaymentController : BaseController
    {
        IConfiguration _configuration;
        IPaymentService _paymentService;
        IOrderService _orderService;
       
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
                payment.OrderId = _paymentService.CreateOrder(payment.GrandTotal * 100, payment.Currency, payment.Description);
            }
            return View(payment);
        }

        [HttpPost]
        public IActionResult Status(IFormCollection form)
        {
            try
            {
                if(form.Keys.Count > 0)
                {
                    string paymentId = form["rzp_paymentid"];
                    string orderid = form["rzp_orderid"];
                    string signature = form["rzp_signature"];
                    string transactionId = form["Receipt"];
                    string currency = form["Currencty"];

                    var payment = _paymentService.GetPaymentDetails(paymentId);
                    bool IsSignedVerified = _paymentService.VerifySignature(signature, orderid, paymentId);

                    if (IsSignedVerified && payment != null)
                    {
                        CartModel cart = TempData.Get<CartModel>(paymentId);
                        PaymentDetail model = new PaymentDetail();

                        model.CartId = cart.Id;
                        model.Total = cart.Total;
                        model.Tax = cart.Tax;
                        model.GrandTotal = cart.GrandTotal;
                        model.CreatedDate = DateTime.Now;

                        model.Status = payment.Attributes["status"];
                        model.TransactionId = transactionId;
                        model.Currency = payment.Attributes["currency"];
                        model.Email = payment.Attributes["email"];
                        model.Id = paymentId;
                        model.UserId = CurrentUser.Id;

                        int status = _paymentService.SavePaymentDetails(model);
                        if (status > 0)
                        {
                            Response.Cookies.Append("CId", "");
                            AddressModel address = TempData.Get<AddressModel>("Address");

                            _orderService.PlaceOrder(CurrentUser.Id, orderid, paymentId, cart, address);
                            TempData.Set("PaymentDetails", model);
                            return RedirectToAction("Receipt");
                        }
                        else
                        {
                            ViewBag.Message = "Internal Server Error!";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Internal Server Error!";
            }
            return View();
           
        }

        public IActionResult Receipt(IFormCollection form)
        {
            PaymentDetail model = TempData.Peek<PaymentDetail>("PaymentDetails");
            return View(model);
        }
    }
}
