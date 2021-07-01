using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildHealthBook.Web.Models.EventDtos
{
    public class WebShareEventCreateDto
    {
        public Guid EventId { get; set; }

        public Guid ParentId { get; set; }
    }
}
