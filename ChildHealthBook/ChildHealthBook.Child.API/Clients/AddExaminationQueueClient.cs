using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using ChildHealthBook.Child.API.DAL;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChildHealthBook.Child.API.Clients
{
    public class AddExaminationQueueClient : BackgroundService
    {
        protected IServiceProvider _serviceProvider;
        private IEventRepository _eventRepository;
        private readonly IChildApiSettings _apiSettings;
        private QueueClient _queueClient;


        public AddExaminationQueueClient([NotNull] IServiceProvider serviceProvider, [NotNull] IEventRepository eventRepository, IChildApiSettings apiSettings)
        {
            _serviceProvider = serviceProvider;
            _eventRepository = eventRepository;
            _apiSettings = apiSettings;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _queueClient = new QueueClient(_apiSettings.StorageConnectionString, _apiSettings.AddNewExaminationQueue);

                    _queueClient.CreateIfNotExists();

                    if (_queueClient.Exists())
                    {

                        QueueMessage[] retrievedMessage = await _queueClient.ReceiveMessagesAsync(2);

                        foreach (var item in retrievedMessage)
                        {
                            await _eventRepository.AddNewExamination(item.MessageText);
                            await _queueClient.DeleteMessageAsync(item.MessageId, item.PopReceipt);
                        }

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

                await Task.Delay(new TimeSpan(0, 1, 0));
            }
        }
    }
}
