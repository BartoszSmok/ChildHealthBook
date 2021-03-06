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
    public class AddNewChildQueueClient : BackgroundService
    {
        protected IServiceProvider _serviceProvider;
        private IChildRepository _childRepository;
        private readonly IChildApiSettings _apiSettings;
        private QueueClient _queueClient;


        public AddNewChildQueueClient([NotNull] IServiceProvider serviceProvider, [NotNull] IChildRepository childRepository, IChildApiSettings apiSettings)
        {
            _serviceProvider = serviceProvider;
            _childRepository = childRepository;
            _apiSettings = apiSettings;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _queueClient = new QueueClient(_apiSettings.StorageConnectionString, _apiSettings.AddNewChildEventQueue);

                    _queueClient.CreateIfNotExists();

                    if (_queueClient.Exists())
                    {

                        QueueMessage[] retrievedMessage = await _queueClient.ReceiveMessagesAsync(2);

                        foreach (var item in retrievedMessage)
                        {
                            await _childRepository.AddNewChild(item.MessageText);
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
