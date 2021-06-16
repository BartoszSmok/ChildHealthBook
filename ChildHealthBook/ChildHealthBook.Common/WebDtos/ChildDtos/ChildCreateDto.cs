using System;

namespace ChildHealthBook.Common.WebDtos.ChildDtos
{
    public class ChildCreateDto
    {
        public Guid ParentId { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string FullName { get; set; }

        public int CurrentWeight { get; set; }

        public int CurrentHeight { get; set; }
    }
}
