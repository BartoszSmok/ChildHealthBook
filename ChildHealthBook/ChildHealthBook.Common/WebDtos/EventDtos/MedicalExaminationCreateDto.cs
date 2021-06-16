using System;

namespace ChildHealthBook.Common.WebDtos.EventDtos
{
    public class MedicalExaminationCreateDto
    {
        public DateTime DateOfMedicalExamination { get; set; }

        public string ExaminationType { get; set; }

        public string ExaminationTitle { get; set; }

        public string Comment { get; set; }

        public string SpecialistFullName { get; set; }

        public string Address { get; set; }
    }
}
