namespace Control_Estadistico_Web.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendInvitationEmailAsync(
            string toEmail,
            string companyName,
            string invitationToken
        );
    }
}
