using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildHealthBook.Web.Models.EventDtos
{
    public class WebPersonalEventReadDto
    {
        public Guid Id { get; set; }

        public Guid ChildId { get; set; }

        public DateTime DateOfEvent { get; set; }

        public string EventType { get; set; }

        public string EventTitle { get; set; }

        public string Comment { get; set; }
    }
}
