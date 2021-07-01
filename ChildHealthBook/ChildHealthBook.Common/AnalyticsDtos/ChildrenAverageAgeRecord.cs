using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ChildHealthBook.Common.AnalyticsDtos
{
    public class ChildrenAverageAgeRecord
    {
        [BsonId]
        public Guid Id { get; set; }
        public float Average { get; set; }
        public DateTime DateOfRecordCreation { get; set; }
    }
}
