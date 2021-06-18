using Azure.Storage.Queues;
using ChildHealthBook.Common.WebDtos.ChildDtos;
using ChildHealthBook.Common.WebDtos.EventDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChildHealthBook.Gateway.API.Clients
{
    public class AzureQueueClient : IAzureQueueClient
    {
        private readonly IGatewayApiSettings _apiSettings;
        private QueueClient _queueClient;

        public AzureQueueClient(IGatewayApiSettings apiSettings)
        {
            _apiSettings = apiSettings;
        }

        public async Task AddNewChild(ChildCreateDto childCreateDto)
        {
            SendMessageToQueue(_apiSettings.AddNewChildEventQueue, JsonSerializer.Serialize(childCreateDto));
        }

        public async Task AddNewExamination(MedicalExaminationCreateDto medicalExaminationCreateDto)
        {
            Console.WriteLine("1a");
            SendMessageToQueue(_apiSettings.AddNewExaminationQueue, JsonSerializer.Serialize(medicalExaminationCreateDto));
        }

        public async Task AddNewMedicalEvent(MedicalEventCreateDto medicalEventCreateDto)
        {
            SendMessageToQueue(_apiSettings.AddNewMedicalEventQueue, JsonSerializer.Serialize(medicalEventCreateDto));
        }

        public async Task AddNewPersonalEvent(PersonalEventCreateDto personalEventCreateDto)
        {
            SendMessageToQueue(_apiSettings.AddNewPersonalEventQueue, JsonSerializer.Serialize(personalEventCreateDto));
        }

        public async Task SendToNotificationService(ExaminationNotificationDto examinationNotificationDto)
        {
            Console.WriteLine("DUPA" + _apiSettings.SendExaminationToNotificationQueue);
            SendMessageToQueue(_apiSettings.SendExaminationToNotificationQueue, JsonSerializer.Serialize(examinationNotificationDto));
        }

        private void SendMessageToQueue(string queueName, string queueMessage)
        {
            try
            {
                _queueClient = new QueueClient(_apiSettings.StorageConnectionString, queueName);

                _queueClient.CreateIfNotExists();

                if (_queueClient.Exists())
                {
                    Console.WriteLine($"Queue created: '{_queueClient.Name}'");
                    _queueClient.SendMessage(queueMessage);
                }
                else
                {
                    Console.WriteLine($"Make sure the Azurite storage emulator running and try again.");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}\n\n");
                Console.WriteLine($"Make sure the Azurite storage emulator running and try again.");
            }
        }
    }
}
