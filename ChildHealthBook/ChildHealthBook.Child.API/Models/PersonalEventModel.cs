using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ChildHealthBook.Child.API.Models
{
    public class PersonalEventModel
    {
        [BsonId]
        public Guid Id { get; set; }

        public Guid ChildId { get; set; }

        public DateTime DateOfEvent { get; set; }

        public string EventType { get; set; }

        public string EventTitle { get; set; }

        public string Comment { get; set; }
    }
}
