using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChildHealthBook.Notification.Service.Models;
using ChildHealthBook.Notification.Service.Settings;
using MongoDB.Driver;

namespace ChildHealthBook.Notification.Service.Data
{
    public class MsgContext : IMsgContext
    {
        private readonly MongoSettings _configuration;

        public MsgContext(MongoSettings configuration)
        {
            var client = new MongoClient(configuration.ConnectionString);
            var database = client.GetDatabase(configuration.DatabaseName);
            msg = database.GetCollection<MsgToSend>(configuration.CollectionName);
        }
        public IMongoCollection<MsgToSend> msg { get; }
    }
}
