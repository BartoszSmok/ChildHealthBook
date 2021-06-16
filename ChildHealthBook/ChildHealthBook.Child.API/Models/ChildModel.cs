using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ChildHealthBook.Child.API.Models
{
    public class ChildModel
    {
        [BsonId]
        public Guid Id { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string FullName { get; set; }

        public int CurrentWeight { get; set; }

        public int CurrentHeight { get; set; }
    }
}
