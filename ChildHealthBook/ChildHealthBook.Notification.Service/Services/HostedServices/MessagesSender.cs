using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChildHealthBook.Notification.Service.Data;
using ChildHealthBook.Notification.Service.Services;
using ChildHealthBook.Notification.Service.Settings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace ChildHealthBook.Notification.Service
{
    public class MessagesSender : BackgroundService
    {
        private readonly ILogger<MessagesSender> _logger;
        private readonly IMsgContext _context;
        private readonly SmtpSettings _smtpSettings;


        public MessagesSender(ILogger<MessagesSender> logger, IMsgContext context, SmtpSettings smtpSettings)
        {
            _logger = logger;
            _context = context;
            _smtpSettings = smtpSettings;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var smtpClient = SmtpClient();
            var msgHandler = new MessagesHandler(_context);

            while (!stoppingToken.IsCancellationRequested)
            {
                var messagesInDb = await msgHandler.GetMsgToSend();
                var now = DateTime.UtcNow;
                var messagesToSent = messagesInDb.Where(x => (x.msg.DateOfMedicalExamination - now).TotalDays <= 1).ToList();
                foreach (var message in messagesToSent)
                {
                    if (message.parent == null)
                    {
                        var info = await msgHandler.GetParentInfoAsync(message.msg);
                        message.parent = info;
                    }
                    try
                    {
                        if (message.parent != null)
                        {
                            Console.WriteLine(message.msg.DateOfMedicalExamination);
                            smtpClient.Send("ChildHealthBook@gmail.com", message.parent.Email, "Reminder of " + message.msg.ExaminationTitle, message.msg.Comment);
                            await msgHandler.DeleteMsg(message);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }

                //smtpClient.Send("ChildHealthBook@gmail.com", "smok.bartosz@gmail.com", messagesInDb[0].ExaminationTitle, messagesInDb[0].Comment);
                //Console.WriteLine("email sent");
                await Task.Delay(60 * 1000, stoppingToken);
            }
        }

        private SmtpClient SmtpClient()
        {
            var smtpClient = new SmtpClient(_smtpSettings.host)
            {
                Port = Convert.ToInt32(_smtpSettings.PortNumber),
                Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                EnableSsl = Convert.ToBoolean(_smtpSettings.EnableSsl)
            };
            return smtpClient;
        }
    }
}
