using ComplaintMngSys.Models;
using System.Threading.Tasks;

namespace ComplaintMngSys.Services
{
    public class EmailSender : IEmailSender
    {
        private IFunctional _functional { get; }
        private readonly ICommon _iCommon;

        public EmailSender(IFunctional functional, ICommon iCommon)
        {
            _functional = functional;
            _iCommon = iCommon;
        }


        public async Task<Task> SendEmailAsync(string email, string subject, string message)
        {
            //sendgrid is become default
            SendGridSetting _sendGridOptions = await _iCommon.GetSendGridEmailSetting();
            if (_sendGridOptions.IsDefault)
            {
                _functional.SendEmailBySendGridAsync(_sendGridOptions.SendGridKey,
                                                    _sendGridOptions.FromEmail,
                                                    _sendGridOptions.FromFullName,
                                                    subject,
                                                    message,
                                                    email)
                                                    .Wait();
            }

            //smtp is become default
            SMTPEmailSetting _smtpOptions = await _iCommon.GetSMTPEmailSetting();
            if (_smtpOptions.IsDefault)
            {
                _functional.SendEmailByGmailAsync(_smtpOptions.FromEmail,
                                            _smtpOptions.FromFullName,
                                            subject,
                                            message,
                                            email,
                                            email,
                                            _smtpOptions.UserName,
                                            _smtpOptions.Password,
                                            _smtpOptions.Host,
                                            _smtpOptions.Port,
                                            _smtpOptions.IsSSL)
                                            .Wait();
            }
            return Task.CompletedTask;
        }
    }
}
