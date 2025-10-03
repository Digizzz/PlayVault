using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Stripe;
using PlayVault.Models;

namespace PlayVault.Controllers
{
    [Route("webhook/stripe")]
    public class StripeWebhookController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ILogger<StripeWebhookController> _logger;

        public StripeWebhookController(IConfiguration config, ILogger<StripeWebhookController> logger)
        {
            _config = config;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var webhookSecret = _config["Stripe:WebhookSecret"];
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], webhookSecret);
                _logger.LogInformation("Stripe event received: {Type}", stripeEvent.Type);

                if (stripeEvent.Type == "checkout.session.completed")
                {
                    var session = stripeEvent.Data.Object as Stripe.Checkout.Session;
                    // TODO: mark donation as paid/fulfilled, persist info in DB if needed
                    _logger.LogInformation("Checkout session completed: {Id}", session?.Id);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Webhook error");
                return BadRequest();
            }
        }
    }
}
