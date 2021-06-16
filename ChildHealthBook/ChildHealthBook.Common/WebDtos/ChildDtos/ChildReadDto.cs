using System;

namespace ChildHealthBook.Common.WebDtos.ChildDtos
{
    public class ChildReadDto
    {
        public Guid Id { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string FullName { get; set; }

        public int CurrentWeight { get; set; }

        public int CurrentHeight { get; set; }
    }
}
