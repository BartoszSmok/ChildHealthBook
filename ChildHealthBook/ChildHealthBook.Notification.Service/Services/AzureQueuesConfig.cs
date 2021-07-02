using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using ChildHealthBook.Notification.Service.Data;
using Newtonsoft.Json;

namespace ChildHealthBook.Notification.Service
{
    public class AzureQueuesConfig
    {
        private readonly IMsgContext _context;

        public AzureQueuesConfig(IMsgContext context)
        {
            _context = context;
        }

        public QueueClient CreateQueue(string connectionString, string queueName)
        {
            try
            {
                QueueClient queueClient = new QueueClient(connectionString, queueName);
                queueClient.CreateIfNotExists();
                return queueClient;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}\n\n");
                Console.WriteLine($"Make sure the Azurite storage emulator running and try again.");
                return null;
            }
        }

        public ExaminationNotificationDto ReciveMsg(QueueClient queueClient)
        {
            if (queueClient.Exists())
            {
                QueueMessage[] retrievedMessage = queueClient.ReceiveMessages();
                if (retrievedMessage.Length == 0)
                    return null;
                var m = retrievedMessage[0].MessageText;
                var message = JsonConvert.DeserializeObject<ExaminationNotificationDto>(m);
                queueClient.DeleteMessage(retrievedMessage[0].MessageId, retrievedMessage[0].PopReceipt);
                return message;
            }
            else
            {
                Console.WriteLine($"Make sure the Azurite storage emulator running and try again.");
                return null;
            }
        }
    }
}
