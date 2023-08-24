using CommonLayer.Models;
using RepoLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IUserBusiness
    {
        public Task<UserEntity> UserRegister(UserRegistrationModel model);
        public Task<UserLogInResult> UserLogin(UserLoginModel loginModel);
        public Task<string> ForgetPassword(ForgetPasswordModel forgetPassword);
        public Task<bool> ResetPassword(ResetPasswordModel resetPassword, string email);
        public string JWTTokenGenerator(long userid, string email);
        public Task<List<UserEntity>> GetAllUserData();
    }
}
