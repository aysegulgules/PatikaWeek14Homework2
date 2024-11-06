using PatikaWeek14Homework2.User.Dtos;
using PatikaWeek14Homework2.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatikaWeek14Homework2.User
{
    public interface IUserService
    {
        Task<ServiceMessage> AddUser(AddUserDto dtoUser);//unit of work kullanılacağı için async

        ServiceMessage<UserInfoDto> LoginUser(LoginUserDto dtoUser);

    }
}
