using CommonLayer.Models;
using RepoLayer.Context;
using RepoLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface IUserRepo
    {
        public UserEntity UserRegister(UserRegistrationModel model);
        public string UserLogin(UserLoginModel loginModel);
        public string JWTTokenGenerator(long userid, string email);
        public List<UserEntity> GetAllUserData();
    }
}
