namespace WalkinPortalAPI.src.Mail
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
