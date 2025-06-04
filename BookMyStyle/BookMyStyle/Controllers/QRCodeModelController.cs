using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using BookMyStyle.Data;
using BookMyStyle.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using QRCoder;

namespace BookMyStyle.Controllers
{
    public class QRCodeModelController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly SmtpSettings _smtpSettings;

        public QRCodeModelController(
            ApplicationDbContext context,
            IWebHostEnvironment env,
            IOptions<SmtpSettings> smtpOptions)
        {
            _context = context;
            _env = env;
            _smtpSettings = smtpOptions.Value;
        }

        // GET: QRCodeModel
        public async Task<IActionResult> Index()
        {
            return View(await _context.QRCodeModels.ToListAsync());
        }

        // GET: QRCodeModel/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: QRCodeModel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QRCodeText")] QRCodeModel qRCodeModel)
        {
            if (!ModelState.IsValid)
            {
                return View(qRCodeModel);
            }


            try
            {
                string callbackUrl = Url.Action(
                    action: "Confirm",
                    controller: "QRCodeModel",
                    values: new { email = qRCodeModel.QRCodeText },
                    protocol: Request.Scheme
                );

                // generisanje qr koda
                using var qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(callbackUrl, QRCodeGenerator.ECCLevel.Q);
                var pngByteQRCode = new PngByteQRCode(qrCodeData);
                byte[] qrCodeBytes = pngByteQRCode.GetGraphic(20);

                
                string wwwroot = _env.WebRootPath;
                string folder = Path.Combine(wwwroot, "GeneratedQRCode");
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                string fileName = $"qrcode_{Guid.NewGuid()}.png";
                string fullPath = Path.Combine(folder, fileName);
                await System.IO.File.WriteAllBytesAsync(fullPath, qrCodeBytes);

                string imageUrl = Url.Content($"~/GeneratedQRCode/{fileName}");
                ViewBag.QRCodeImage = imageUrl;
                ViewBag.CallbackUrl = callbackUrl;

               
                _context.QRCodeModels.Add(qRCodeModel);
                await _context.SaveChangesAsync();

               
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                
                return View(qRCodeModel);
            }
        }

        // GET: QRCodeModel/Confirm?email=neko@primjer.ba
        [HttpGet]
        public IActionResult Confirm(string email)
        {
            if (string.IsNullOrEmpty(email) || !IsValidEmail(email))
            {
                return Content("Neispravan poziv potvrde.");
            }

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
                        <p>Vaš QR kod je uspješno skeniran i uspješno je potvrđena Vaša rezeravacija termina.</p>
                        <p>Poslali smo potvrdu na: <strong>{toEmail}</strong></p>
                        <p>Hvala što koristite naš sistem BookMyStyle!</p>"
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