using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatikaWeek14Homework2.User.Dtos
{
    public class LoginUserDto
    {
     
        public string Email { get; set; }
     
        public string Password { get; set; }
    }
}
