using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe;
using ASPBoilerplate.Services.Stripe;
namespace StripeCheckoutDemo.Controllers
{
    [ApiController]
    [Route("/stripe/checkout")]
    public class CheckoutController : Controller
    {
        private readonly IConfiguration _configuration;
        public CheckoutController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost("create-checkout-session")]
        // This checkout Formmodel I can be creative with
        public IActionResult CreateCheckoutSession([FromBody] CheckoutFormModel model)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = model.Currency,
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = model.ProductName,
                            Description = model.ProductDescription,
                        },
                        UnitAmount = model.Amount,
                    },
                    Quantity = 1,
                },
            },
                Mode = "payment",
                // I somehow need to know how to pass the data through here
                SuccessUrl = $"{Request.Scheme}://{Request.Host}/checkout/success",
                CancelUrl = $"{Request.Scheme}://{Request.Host}/checkout/cancel",
            };
            var service = new SessionService();
            var session = service.Create(options);
            return Ok(new { sessionId = session.Id });
        }
        [HttpGet("success")]
        public IActionResult Success()
        {
            // This needs to redirect somewhere, also all the success operations should go to here
            return View();
        }
        [HttpGet("cancel")]
        public IActionResult Cancel()
        {
            // All the failure operations should go to here
            return View();
        }
    }
}