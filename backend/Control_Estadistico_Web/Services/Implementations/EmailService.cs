using Control_Estadistico_Web.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Control_Estadistico_Web.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendInvitationEmailAsync(
            string toEmail,
            string companyName,
            string invitationToken)
        {
            var frontendUrl = _configuration["Frontend:BaseUrl"];
            var host = _configuration["CONFIGURACIONES_EMAIL:HOST"];
            var port = int.Parse(_configuration["CONFIGURACIONES_EMAIL:PORT"]!);
            var fromEmail = _configuration["CONFIGURACIONES_EMAIL:EMAIL"];
            var password = _configuration["CONFIGURACIONES_EMAIL:PASSWORD"];

            var invitationLink =
                $"{frontendUrl}/auth/register/invitation?token={invitationToken}";


            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(fromEmail));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = $"Invitación para unirte a {companyName}";

            message.Body = new TextPart("plain")
            {
                Text = $@"
                Has sido invitado a unirte a la empresa {companyName}.

                Para completar tu registro, haz clic en el siguiente enlace:
                {invitationLink}

                Este enlace expirará en 7 días.

                Si no esperabas este correo, puedes ignorarlo.
                "
            };

            using var client = new SmtpClient();

            await client.ConnectAsync(host, port, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(fromEmail, password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
