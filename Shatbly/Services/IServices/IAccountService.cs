namespace Shatbly.Services.IServices
{
    public interface IAccountService
    {
       
            Task SendEmailAsync(EmailType emailType, string msg, User applicationUser);
        
    }
}
