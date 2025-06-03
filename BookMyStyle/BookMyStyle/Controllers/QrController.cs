using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// Ovo mora biti tu, da se vidi QRCodeGenerator i QRCode tipovi
using QRCoder;

// Ako niste, dodajte i:
using System.Drawing;               // za Bitmap
using System.Drawing.Imaging;       // za ImageFormat
using BookMyStyle.Models;


namespace BookMyStyle.Controllers
{
    public class QrController : Controller
    {
        private readonly IConfiguration _config;

        public QrController(IConfiguration config)
        {
            _config = config;
        }

        // GET: /Qr/Generate
        [HttpGet]
        public IActionResult Generate()
        {
            // Vrati prazan model – forma se renderira bez QR koda
            return View(new EmailQrViewModel());
        }

        // POST: /Qr/Generate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Generate(EmailQrViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Ako validacija emaila ne prođe, prikaži formu s greškama.
                return View(model);
            }

            // 1) Generiraj URL koji se enkodira u QR kod – primjer: 
            //    https://<tvoj-domen>/Reservation/Confirm?email={model.Email}
            // U praksi zamijeni "Reservation" i "Confirm" akciju s onom koju stvarno koristiš.
            string url = Url.Action(
                action: "Confirm",
                controller: "Reservation",
                values: new { email = model.Email },
                protocol: Request.Scheme);

            // 2) Generiraj QR kod od URL‐a
            string base64Qr;
            using (var qrGenerator = new QRCodeGenerator())
            using (var qrData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q))
            using (var qrCode = new QRCode(qrData))
            using (var bitmap = qrCode.GetGraphic(20)) // "20" određuje visinu/širinu modula
            using (var ms = new MemoryStream())
            {
                // Spremi sliku u memorijski stream kao PNG
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();
                base64Qr = Convert.ToBase64String(imageBytes);
            }

            // 3) Sačuvaj Base64 string u model, da ga View može prikazati
            model.QrCodeImageBase64 = $"data:image/png;base64,{base64Qr}";

            // 4) Pošalji email korisniku s QR kodom priloženim kao attachment
            try
            {
                SendConfirmationEmailWithQr(model.Email, base64Qr);
                ViewData["SuccessMessage"] = "QR kod je poslan na Vaš email.";
            }
            catch (Exception ex)
            {
                // Ako pošalje nije uspio, ispiši grešku, ali View se i dalje vraća
                ViewData["ErrorMessage"] = "Dogodila se pogreška pri slanju emaila: " + ex.Message;
            }

            return View(model);
        }

        private void SendConfirmationEmailWithQr(string toEmail, string base64Qr)
        {
            // 1) Unesi svoje SMTP postavke u appsettings.json ili tajno (user secret)
            //    Primjer:
            //    "SmtpSettings": {
            //       "Host": "smtp.mailtrap.io", 
            //       "Port": 587, 
            //       "Username": "tvoj-smtp-user", 
            //       "Password": "tvoj-smtp-pass", 
            //       "From": "noreply@bookmystyle.com"
            //    }
            //
            var smtpHost = _config["SmtpSettings:Host"];
            var smtpPort = int.Parse(_config["SmtpSettings:Port"] ?? "587");
            var smtpUser = _config["SmtpSettings:Username"];
            var smtpPass = _config["SmtpSettings:Password"];
            var fromAddress = _config["SmtpSettings:From"];

            using (var client = new SmtpClient(smtpHost, smtpPort))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(smtpUser, smtpPass);

                // 2) Pripremi MailMessage
                var mail = new MailMessage();
                mail.From = new MailAddress(fromAddress, "BookMyStyle");
                mail.To.Add(toEmail);
                mail.Subject = "Potvrda rezervacije – Vaš QR kod";
                mail.Body = $"Poštovani,\n\nHvala što ste izvršili rezervaciju. U privitku se nalazi QR kod koji Vam omogućuje prijavu.\n\nSrdačan pozdrav,\nBookMyStyle tim";

                // 3) Pretvori Base64 nazad u byte[] i priloži kao PNG attachment
                byte[] qrBytes = Convert.FromBase64String(base64Qr);
                using (var ms = new MemoryStream(qrBytes))
                {
                    var attachment = new Attachment(ms, "qr-code.png", "image/png");
                    mail.Attachments.Add(attachment);

                    // 4) Pošalji
                    client.Send(mail);
                }
            }
        }
    }
}
