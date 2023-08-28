using BusinessLayer.Interface;
using CommonLayer.Models;
using RepoLayer.Entities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepo userRepo;
        public UserBusiness(IUserRepo userRepo)
        {
            this.userRepo = userRepo;
        }

        public async Task<UserEntity> UserRegister(UserRegistrationModel model)
        {
            try
            {
                return await userRepo.UserRegister(model);
            }
            catch (Exception)
            {

                throw;
            }
            //throw new NotImplementedException();
        }
        public async Task<UserLogInResult> UserLogin(UserLoginModel loginModel)
        {
            try
            {
                return await userRepo.UserLogin(loginModel);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<string> JWTTokenGenerator(long userid, string email)
        {
            try
            {
                return await userRepo.JWTTokenGenerator(userid, email);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<string> ForgetPassword(ForgetPasswordModel forgetPassword)
        {
            try
            {
                return await userRepo.ForgetPassword(forgetPassword);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> ResetPassword(ResetPasswordModel resetPassword, string email)
        {
            try
            {
                return await userRepo.ResetPassword(resetPassword, email);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
