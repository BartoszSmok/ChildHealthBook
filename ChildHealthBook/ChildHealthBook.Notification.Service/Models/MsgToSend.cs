using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ChildHealthBook.Notification.Service.Models
{
    public class MsgToSend
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public ExaminationNotificationDto msg { get; set; }
        public ParentsDto parent { get; set; }
    }
}
