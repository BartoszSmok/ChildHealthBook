using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildHealthBook.Child.API.Models
{
    public class ShareEventModel
    {
        [BsonId]
        public Guid ShareId { get; set; }

        public Guid EventId { get; set; }

        public Guid ParentId { get; set; }
    }
}
