using BookMyStyle.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using QRCoder;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace BookMyStyle.Controllers
{
    public class QRCodeModelController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly SmtpSettings _smtpSettings;

        public QRCodeModelController(
            IWebHostEnvironment env,
            IOptions<SmtpSettings> smtpOptions)
        {
            _env = env;
            _smtpSettings = smtpOptions.Value;
        }

        // GET: QRCodeModel/Create
        public IActionResult Create()
        {
            // CHANGED: uklonili smo pogrešan View("")
            return View();
        }

        // POST: QRCodeModel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("QRCodeText")] QRCodeModel qRCodeModel)
        {
            if (!ModelState.IsValid)
                return View(qRCodeModel);

            // 1) Generiraj URL za Confirm
            string callbackUrl = Url.Action(
                action: "Confirm",
                controller: "QRCodeModel",
                values: new { email = qRCodeModel.QRCodeText },
                protocol: Request.Scheme
            );

            // 2) Generiranje QR koda
            using var qrGenerator = new QRCodeGenerator();
            var qrData = qrGenerator.CreateQrCode(callbackUrl, QRCodeGenerator.ECCLevel.Q);
            var png = new PngByteQRCode(qrData);
            byte[] qrBytes = png.GetGraphic(20);

            // 3) Spremi sliku u wwwroot/GeneratedQRCode
            string wwwroot = _env.WebRootPath;
            string folder = Path.Combine(wwwroot, "GeneratedQRCode");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string fileName = $"qrcode_{Guid.NewGuid()}.png";
            string fullPath = Path.Combine(folder, fileName);
            System.IO.File.WriteAllBytes(fullPath, qrBytes);

            // 4) Proslijedi URL slike i Confirm link u ViewBag
            ViewBag.QRCodeImage = Url.Content($"~/GeneratedQRCode/{fileName}");
            ViewBag.CallbackUrl = callbackUrl;

            // CHANGED: vraćamo se u Create.cshtml (s modelom), gdje ćete prikazati sliku i link
            return View(qRCodeModel);
        }

        // GET: QRCodeModel/Confirm?email=neko@primjer.ba
        [HttpGet]
        public IActionResult Confirm(string email)
        {
            if (string.IsNullOrEmpty(email) || !IsValidEmail(email))
                return Content("Neispravan poziv potvrde.");

            try
            {
                SendConfirmationEmail(email);
                ViewBag.EmailStatus = "E-mail je uspješno poslan.";
            }
            catch (Exception ex)
            {
                ViewBag.EmailStatus = $"Greška pri slanju e-maila: {ex.GetType().Name} – {ex.Message}";
            }

            ViewBag.ConfirmedEmail = email;
            return View("Confirmed");
        }

        #region Helper Methods

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void SendConfirmationEmail(string toEmail)
        {
            var msg = new MailMessage
            {
                From = new MailAddress(_smtpSettings.From, _smtpSettings.DisplayName),
                Subject = "Potvrda o uspješnom skeniranju QR koda",
                IsBodyHtml = true,
                Body = $@"
                    <h3>Dragi/a korisniče,</h3>
                    <p>Vaš QR kod je uspješno skeniran i potvrđena je Vaša rezervacija.</p>
                    <p>Poslali smo potvrdu na: <strong>{toEmail}</strong></p>
                    <p>Hvala što koristite BookMyStyle!</p>"
            };
            msg.To.Add(new MailAddress(toEmail));

            using var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
            {
                EnableSsl = _smtpSettings.EnableSsl,
                Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password)
            };
            client.Send(msg);
        }

        #endregion
    }
}
