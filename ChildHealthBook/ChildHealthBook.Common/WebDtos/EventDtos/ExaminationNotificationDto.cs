using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildHealthBook.Common.WebDtos.EventDtos
{
    public class ExaminationNotificationDto
    {
        public Guid ParentId { get; set; }

        public string ChildFullName { get; set; }

        public DateTime DateOfMedicalExamination { get; set; }

        public string ExaminationType { get; set; }

        public string ExaminationTitle { get; set; }

        public string Comment { get; set; }

        public string SpecialistFullName { get; set; }

        public string Address { get; set; }
    }
}
