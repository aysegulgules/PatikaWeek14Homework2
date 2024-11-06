using PatikaWeek14Homework2.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatikaWeek14Homework2.User.Dtos
{
    //Giriş yapıldıklan sonra Token oluşturmak için kullanılır.
    public class UserInfoDto
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public UserType UserType { get; set; }

    }
}
