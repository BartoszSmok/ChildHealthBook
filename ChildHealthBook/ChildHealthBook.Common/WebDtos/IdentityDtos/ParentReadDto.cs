using System;

namespace ChildHealthBook.Common.WebDtos.IdentityDtos
{
    public class ParentReadDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }
    }
}
