using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatikaWeek14Homework2.User.Dtos
{
    public class AddUserDto
    {
        
        public string Email { get; set; }
        
        public string Password { get; set; }
       
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

    }
}
