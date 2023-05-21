

using PayRollSystem.Domain.EmailRepositories;


namespace PayRollSystem.Domain.IPayRollSystemServices

{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailMessage message);
    }
}
