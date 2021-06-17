using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ChildHealthBook.Child.API.Models
{
    public class ExaminationModel
    {
        [BsonId]
        public Guid Id { get; set; }

        public Guid ChildId { get; set; }

        public DateTime DateOfMedicalExamination { get; set; }

        public string ExaminationType { get; set; }

        public string ExaminationTitle { get; set; }

        public string Comment { get; set; }

        public string SpecialistFullName { get; set; }

        public string Address { get; set; }
    }
}
