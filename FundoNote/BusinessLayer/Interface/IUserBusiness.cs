using CommonLayer.Models;
using RepoLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBusiness
    {
        public UserEntity UserRegister(UserRegistrationModel model);
        public string UserLogin(UserLoginModel loginModel);
        public string JWTTokenGenerator(long userid, string email);
        public List<UserEntity> GetAllUserData();
    }
}
