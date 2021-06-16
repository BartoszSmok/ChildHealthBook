using System;

namespace ChildHealthBook.Common.WebDtos.EventDtos
{
    public class MedicalEventCreateDto
    {
        public DateTime DateOfEvent { get; set; }

        public string EventType { get; set; }

        public string EventTitle { get; set; }

        public string Comment { get; set; }
    }
}
