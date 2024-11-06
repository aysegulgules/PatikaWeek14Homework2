using PatikaWeek14Homework2.DataProtection;
using PatikaWeek14Homework2.User.Dtos;
using PatikaWeek14Homework2.Types;
using PatikaWeek14Homework2.Data.Entities;
using PatikaWeek14Homework2.Data.Enums;
using PatikaWeek14Homework2.Data.Repositories;
using PatikaWeek14Homework2.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatikaWeek14Homework2.User
{
    public class UserManager :IUserService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IRepository<UserEntity> _userRepository;
        public readonly IDataProtection _protection;

        public UserManager(IRepository<UserEntity> userRepository,IUnitOfWork unitOfWork, IDataProtection protection)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _protection = protection;
        }

        public async Task<ServiceMessage> AddUser(AddUserDto userDto)
        {
            var hasMail = _userRepository.GetAll(x => x.Email.ToLower() == userDto.Email.ToLower());

            if (hasMail.Any())
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Email mevcut"
                };
            }

            var userEntity = new UserEntity()
            {
                Email = userDto.Email,
               
                Password = _protection.Protect(userDto.Password),

                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                BirthDate = userDto.BirthDate,
            
            };
            _userRepository.Add(userEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();

            }
            catch (Exception ex) 
            {
                throw new Exception("Kullanıcı kaydı sırasında bir hata oluştu.");
            }

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Kullanıcı kaydedildi."
            };


        }

        public ServiceMessage<UserInfoDto> LoginUser(LoginUserDto dtoUser)
        {
            var userEntity = _userRepository.Get(x => x.Email.ToLower() == dtoUser.Email);

            if(userEntity is null)
            {
                return new ServiceMessage <UserInfoDto> 
                {
                    IsSucceed = false,
                   Message = "Kullanıcı adı ve şifre hatalı" 
                };
            }

            var unprotectedPassword= _protection.UnProtect(userEntity.Password);

            if(unprotectedPassword !=dtoUser.Password)
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = false,
                    Message = "Kullanıcı adı ve şifre hatalı"
                };
            }
            else
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = true,
                    Data=new UserInfoDto
                    {
                        Email = userEntity.Email,
                        FirstName = userEntity.FirstName,
                        LastName = userEntity.LastName,
                        UserType = userEntity.UserType,

                    }
                };

            }
        }
    }
}
