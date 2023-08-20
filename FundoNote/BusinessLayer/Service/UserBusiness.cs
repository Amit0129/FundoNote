using BusinessLayer.Interface;
using CommonLayer.Models;
using RepoLayer.Entities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepo userRepo;
        public UserBusiness(IUserRepo userRepo)
        {
            this.userRepo = userRepo;   
        }

        public UserEntity UserRegister(UserRegistrationModel model)
        {
            try
            {
                return userRepo.UserRegister(model);
            }
            catch (Exception)
            {

                throw;
            }
            //throw new NotImplementedException();
        }
        public string UserLogin(UserLoginModel loginModel)
        {
            try
            {
                return userRepo.UserLogin(loginModel);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string JWTTokenGenerator(long userid, string email)
        {
            try
            {
                return userRepo.JWTTokenGenerator(userid, email);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string ForgetPassword(ForgetPasswordModel forgetPassword)
        {
            try
            {
                return userRepo.ForgetPassword(forgetPassword);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool ResetPassword(ResetPasswordModel resetPassword, string email)
        {
            try
            {
                return userRepo.ResetPassword(resetPassword, email);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<UserEntity> GetAllUserData()
        {
            try
            {
                return userRepo.GetAllUserData();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
