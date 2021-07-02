using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChildHealthBook.Notification.Service.Data;
using ChildHealthBook.Notification.Service.Models;
using ChildHealthBook.Notification.Service.Services;
using ChildHealthBook.Notification.Service.Settings;

namespace ChildHealthBook.Notification.Service
{
    public class MessagesReciver : BackgroundService
    {
        private readonly ILogger<MessagesReciver> _logger;
        private readonly AzureQueueConfigData _data;
        private readonly IMsgContext _context;


        public MessagesReciver(ILogger<MessagesReciver> logger, AzureQueueConfigData data, IMsgContext context)
        {
            _logger = logger;
            _data = data;
            _context = context;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var messageHandler = new MessagesHandler(_context);
            var azureQueuesConfig = new AzureQueuesConfig(_context);
            var queueClient = azureQueuesConfig.CreateQueue(_data.CS, _data.queueName);
            while (!stoppingToken.IsCancellationRequested)
            {
                //var messageRecived = azureQueuesConfig.ReciveMsg(queueClient);
                Console.WriteLine(DateTime.Today.Add(TimeSpan.FromHours(24)));
                var messageRecived = new ExaminationNotificationDto()
                    {ExaminationTitle = "badanie", Comment = "komentarz testowy", DateOfMedicalExamination = DateTime.Today.Add(TimeSpan.FromHours(72))};
                //var parentInfo = await messageHandler.GetParentInfoAsync(messageRecived);
                //var msgToSend = messageHandler.CreateMsgToSend(messageRecived, parentInfo);
                var parentInfo = new ParentsDto() {Email = "smok.bartosz@gmail.com"};
                var msgToSend = new MsgToSend() {msg = messageRecived, parent = parentInfo};
                Console.WriteLine("working ?");
                await messageHandler.AddMsgToDb(msgToSend);
                await Task.Delay(60 * 1000, stoppingToken);
            }
        }
    }
}


//var msg = new TestMessage() { str = "test" };
//var serializedVal = JsonConvert.SerializeObject(msg);
//queueClient.SendMessage(serializedVal);
