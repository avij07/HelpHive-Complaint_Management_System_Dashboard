using System.Threading.Tasks;

namespace ComplaintMngSys.Services
{
    public interface IEmailSender
    {
        Task<Task> SendEmailAsync(string email, string subject, string message);
    }
}
