using System;

namespace ChildHealthBook.Common.WebDtos.EventDtos
{
    public class ShareEventCreateDto
    {
        public Guid EventId { get; set; }

        public Guid ParentId { get; set; }
    }
}
