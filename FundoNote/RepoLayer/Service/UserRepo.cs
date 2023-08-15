using CommonLayer;
using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepoLayer.Context;
using RepoLayer.Entities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;

namespace RepoLayer.Service
{
    public class UserRepo : IUserRepo
    {
        private readonly FundoContext fundoContext;
        private readonly IConfiguration Iconfiguration;
        public UserRepo(FundoContext fundoContext, IConfiguration Iconfiguration)
        {
            this.fundoContext = fundoContext;
            this.Iconfiguration = Iconfiguration;
        }
        //public string EncryptPassword(string password)
        //{
        //    try
        //    {
        //        byte[] encode = new byte[password.Length];
        //        encode = Encoding.UTF8.GetBytes(password);
        //        string encriptPassword = Convert.ToBase64String(encode);
        //        return encriptPassword;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        public UserEntity UserRegister(UserRegistrationModel model)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName= model.FirstName;
                userEntity.LastName= model.LastName;
                userEntity.Email= model.Email;
                userEntity.Password=EncryptAndDrcrypy.ConvertToEncrypt(model.Password);

                fundoContext.Users.Add(userEntity);
                fundoContext.SaveChanges();
                if (userEntity != null)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        //User Log in=========================
        public string UserLogin(UserLoginModel loginModel)
        {
            try
            {
                UserEntity userEntityLogin = new UserEntity();
                var encryptPassword = EncryptAndDrcrypy.ConvertToEncrypt(loginModel.Password);
                userEntityLogin = fundoContext.Users.FirstOrDefault(x=>x.Email == loginModel.Email && x.Password == encryptPassword);
                
                if (userEntityLogin == null)
                {
                    return null;
                }
                else 
                {
                    var id = userEntityLogin.UserID;
                    var email = userEntityLogin.Email;
                    var token = JWTTokenGenerator(id, email);
                    return token;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Generate JWT Token=============================================
        public string JWTTokenGenerator(long userid, string email)
        {
            var tokenHanler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Iconfiguration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("UserId", userid.ToString()),
                    new Claim(ClaimTypes.Email, email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHanler.CreateToken(tokenDescriptor);
            return tokenHanler.WriteToken(token);
        }
        //Get All User Data========================
        public List<UserEntity> GetAllUserData()
        {
            try
            {
                //UserEntity userEntityLogin = new UserEntity();
                var userData = fundoContext.Users.ToList();
                if (userData != null)
                {
                    return userData;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
