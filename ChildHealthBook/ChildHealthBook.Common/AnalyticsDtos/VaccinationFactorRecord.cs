using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ChildHealthBook.Common.AnalyticsDtos
{
    public class VaccinationFactorRecord
    {
        [BsonId]
        public Guid Id { get; set; }
        public float Factor { get; set; }
        public DateTime DateOfRecordCreation { get; set; }
    }
}
