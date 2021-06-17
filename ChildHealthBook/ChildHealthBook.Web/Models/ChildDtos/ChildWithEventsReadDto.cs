using ChildHealthBook.Web.Models.EventDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildHealthBook.Web.Models.ChildDtos
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
