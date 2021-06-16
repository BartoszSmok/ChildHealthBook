using System;

namespace ChildHealthBook.Common.WebDtos.EventDtos
{
    class MedicalEventReadDto
    {
        public int Id { get; set; }

        public int ChildId { get; set; }

        public DateTime DateOfEvent { get; set; }

        public string EventType { get; set; }

        public string EventTitle { get; set; }

        public string Comment { get; set; }
    }
}
