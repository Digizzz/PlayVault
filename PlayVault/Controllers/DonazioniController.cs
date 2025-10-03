using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Stripe;
using Stripe.Checkout;

namespace PlayVault.Controllers
{
    public class DonazioniController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ILogger<DonazioniController> _logger;

        public DonazioniController(IConfiguration config, ILogger<DonazioniController> logger)
        {
            _config = config;
            _logger = logger;
            // initialize stripe api key
            StripeConfiguration.ApiKey = _config["Stripe:SecretKey"];
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCheckout(decimal amount)
        {
            if (amount <= 0)
            {
                TempData["ErrorMessage"] = "Importo non valido.";
                return RedirectToAction("Index");
            }

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(amount * 100), // amount in cents
                            Currency = "eur",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Donazione a Play Vault"
                            }
                        },
                        Quantity = 1
                    }
                },
                Mode = "payment",
                SuccessUrl = Url.Action("Success", "Donazioni", null, Request.Scheme),
                CancelUrl = Url.Action("Cancel", "Donazioni", null, Request.Scheme)
            };

            var service = new SessionService();
            var session = service.Create(options);

            if (session == null || string.IsNullOrEmpty(session.Url))
            {
                TempData["ErrorMessage"] = "Errore nella creazione della sessione di pagamento.";
                return RedirectToAction("Index");
            }

            return Redirect(session.Url);
        }

        [HttpGet]
        public IActionResult Success()
        {
            return View("Success");
        }

        [HttpGet]
        public IActionResult Cancel()
        {
            return View("Cancel");
        }
    }
}
