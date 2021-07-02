using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChildHealthBook.Notification.Service.Models;
using MongoDB.Driver;

namespace ChildHealthBook.Notification.Service.Data
{
    public interface IMsgContext
    {
        IMongoCollection<MsgToSend> msg { get; }
    }
}
