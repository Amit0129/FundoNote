using CommonLayer.Models;
using RepoLayer.Context;
using RepoLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Interface
{
    public interface IUserRepo
    {
        public Task<UserEntity> UserRegister(UserRegistrationModel model);
        public Task<UserLogInResult> UserLogin(UserLoginModel loginModel);
        public Task<string> ForgetPassword(ForgetPasswordModel forgetPassword);
        public Task<bool> ResetPassword(ResetPasswordModel resetPassword, string email);
        public Task<string> JWTTokenGenerator(long userid, string email);
    }
}
