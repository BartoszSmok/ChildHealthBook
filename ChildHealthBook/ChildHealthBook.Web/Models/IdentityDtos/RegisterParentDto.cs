using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildHealthBook.Web.Models.IdentityDtos
{
    public class RegisterParentDto
    {
        
        public string Login { get; set; }

      
        public string Password { get; set; }

       
        public string confirmPassword { get; set; }

        
        public string FirstName { get; set; }

      
        public string LastName { get; set; }

       
        public DateTime DateOfBirth { get; set; }

       
        public string Phone { get; set; }

        
        public string Email { get; set; }
    }
}
