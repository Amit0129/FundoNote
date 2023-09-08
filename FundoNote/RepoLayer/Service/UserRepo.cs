using CommonLayer;
using CommonLayer.Models;
using Microsoft.EntityFrameworkCore;
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
using System.Threading.Tasks;

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
        public async Task<UserEntity> UserRegister(UserRegistrationModel model)
        {
            try
            {
                UserEntity userEntity = new UserEntity
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = EncryptAndDrcrypy.ConvertToEncrypt(model.Password)
                };

                await fundoContext.Users.AddAsync(userEntity);
                await fundoContext.SaveChangesAsync();
                if (userEntity != null)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //User Log in=========================
        public async Task<UserLogInResult> UserLogin(UserLoginModel loginModel)
        {
            try
            {
                var encryptPassword = EncryptAndDrcrypy.ConvertToEncrypt(loginModel.Password);
                var userEntityLogin = await fundoContext.Users.FirstOrDefaultAsync(x => x.Email == loginModel.Email && x.Password == encryptPassword);

                if (userEntityLogin == null)
                {
                    return null;
                }
                else
                {
                    return new UserLogInResult()
                    {
                        UserEntity = userEntityLogin,
                        Token = await JWTTokenGenerator(userEntityLogin.UserID, userEntityLogin.Email)
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Generate JWT Token=============================================
        public async Task<string> JWTTokenGenerator(long userid, string email)
        {
            var tokenHanler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Iconfiguration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("UserId", userid.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(25),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHanler.CreateToken(tokenDescriptor);
            return tokenHanler.WriteToken(token);
        }
        //Forget Password==========================
        public async Task<string> ForgetPassword(ForgetPasswordModel forgetPassword)
        {
            try
            {
                var user = await fundoContext.Users.Where(x => x.Email == forgetPassword.Email).FirstOrDefaultAsync();
                var userEmail = user.Email;
                var id = user.UserID;
                if (user != null)
                {
                    var token = await JWTTokenGenerator(id, userEmail);
                    MSMQ msmq = new MSMQ();
                    msmq.sendData2Queue(token);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //ResetPassword ===================
        public async Task<bool> ResetPassword(ResetPasswordModel resetPassword, string email)
        {
            try
            {
                UserEntity user = new UserEntity();
                user = await fundoContext.Users.FirstOrDefaultAsync(x => x.Email == email);
                if (user != null && resetPassword.NewPassword == resetPassword.ConfirmPassword)
                {
                    user.Password = EncryptAndDrcrypy.ConvertToEncrypt(resetPassword.NewPassword);
                    await fundoContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
