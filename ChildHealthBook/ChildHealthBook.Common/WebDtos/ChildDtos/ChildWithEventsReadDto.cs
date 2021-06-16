using ChildHealthBook.Common.WebDtos.EventDtos;
using System;
using System.Collections.Generic;

namespace ChildHealthBook.Common.WebDtos.ChildDtos
{
    public class ChildWithEventsReadDto
    {
        public Guid Id { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string FullName { get; set; }

        public int CurrentWeight { get; set; }

        public int CurrentHeight { get; set; }

        public IEnumerable<MedicalExaminationReadDto> MedicalExaminations { get; set; }

        public IEnumerable<PersonalEventReadDto> PersonalEvents { get; set; }

        public IEnumerable<MedicalEventReadDto> MedicalEvents { get; set; }
    }
}
