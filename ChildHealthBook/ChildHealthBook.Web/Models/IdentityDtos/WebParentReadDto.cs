using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildHealthBook.Web.Models.IdentityDtos
{
    public class WebParentReadDto
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }
    }
}
