using System.Net.Mail;
using ApiServer.Contracts.DaysOff;
using Domain.Model;

namespace Domain.Services.Interfaces.Services
{
    public interface IEmailSenderService
    {
        MailMessage ComposeDaysOffMail(DaysOff daysoff);
        void SendMail(MailMessage mail);
    }
}
