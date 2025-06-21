using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp
{
    public class SendGridService
    {
        private readonly string _apiKey = "";
        private readonly string _fromEmail = "joseph.pastora@gmail.com";
        private readonly string _fromName = "CenfoCinemas";

        public async Task<bool> SendWelcomeEmail(string toEmail, string toName)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress(_fromEmail, _fromName);
            var to = new EmailAddress(toEmail, toName);
            var subject = "¡Bienvenido!";
            var plainText = $"Hola {toName}, gracias por registrarte.";
            var htmlContent = $"<strong>Hola {toName},</strong><br/>¡Gracias por unirte a nosotros!";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainText, htmlContent);
            var response = await client.SendEmailAsync(msg);

            return response.StatusCode == System.Net.HttpStatusCode.Accepted;
        }


        public async Task<bool> SendNewMovieNotification(string toEmail, string toName, string movieTitle, string genre, DateTime releaseDate)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress(_fromEmail, _fromName);
            var to = new EmailAddress(toEmail, toName);
            var subject = $"🎬 Nueva película agregada: {movieTitle}";

            var plainText = $"Hola {toName}, se ha agregado una nueva película: {movieTitle}, género: {genre}, estreno: {releaseDate:dd/MM/yyyy}.";
            var htmlContent = $@"
                                <strong>Hola {toName},</strong><br/>
                                ¡Tenemos una nueva película en cartelera!<br/><br/>
                                <b>Título:</b> {movieTitle}<br/>
                                <b>Género:</b> {genre}<br/>
                                <b>Fecha de estreno:</b> {releaseDate:dd/MM/yyyy}<br/><br/>
                                ¡No te la pierdas en <i>CenfoCinemas</i>!";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainText, htmlContent);
            var response = await client.SendEmailAsync(msg);

            return response.StatusCode == System.Net.HttpStatusCode.Accepted;
        }

    }


}
