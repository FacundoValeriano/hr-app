using ApiServer.Contracts.DaysOff;
using AutoMapper;
using Core;
using Domain.Model;
using Domain.Services.Interfaces.Services;
using System;
using System.Net.Mail;
using MimeKit;
using MailKit.Net.Smtp;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Domain.Services.Impl.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IMapper _mapper;
        private readonly ILog<DaysOffService> _log;
        public EmailSenderService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public MailMessage ComposeDaysOffMail(DaysOff daysoff)
        {
            MailAddress from = new MailAddress("HRMockupAdress@softvision.com");
            MailAddress to = new MailAddress(daysoff.Employee.EmailAddress);
            //MailAddress cc = new MailAddress("mockup@address"); 

            var message = new MailMessage(from, to);
            message.IsBodyHtml = false;
            //message.CC.Add(cc); Could be used in the future to send a carbon copy to the employee's project manager
            message.Subject = $"Solicitud de días de licencia: {daysoff.Date} - {daysoff.EndDate}. Estado: {daysoff.Status}";
            message.Body = $"{daysoff.Employee.Name}, \n\tEl propósito del presente correo es comunicarle que su solicitud de días de licencia con inicio {daysoff.Date} y fin {daysoff.EndDate}, con motivo de {daysoff.Type}," +
                $"se encuentra ahora {daysoff.Status}. \n\t\tLo saluda atte.\n\t\t\tEl equipo de Cognizant Softvision ";

            return message;
        }

        public void SendMail(MailMessage mail)
        {
            string smtpServer = "smtp.gmail.com";            //Host
            int smtpPort = 587;                              //Port number. Usually 25, 465, or 587.
            string smtpUsername = "thomasnazar@gmail.com";   //MailAdress Credentials
            string smtpPassword = "TheBreaker0220-";         //MailAdress Credentials

            SmtpClient client = new SmtpClient();
            
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            client.Connect(smtpServer, smtpPort);
            client.Authenticate(smtpUsername, smtpPassword);
            client.Send((MimeMessage)mail); 
            
            client.Disconnect(true);
        }
    }
}
