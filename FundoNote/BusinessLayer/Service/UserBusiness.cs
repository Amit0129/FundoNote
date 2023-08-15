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
                return userRepo.UserRegister(model);//ASK
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
