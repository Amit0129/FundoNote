using BusinessLayer.Interface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FundoNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness userBusiness;
        public UserController(IUserBusiness userBusiness)
        {
            this.userBusiness = userBusiness;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> UserRegister(UserRegistrationModel model)
        {
            try
            {
                var result = await userBusiness.UserRegister(model);
                if (result != null)
                {
                    return Ok(new { sucess = true, message = "User Registration Sucesssfull", data = result });
                }
                else
                {
                    return BadRequest(new { sucess = false, message = "User Registration Unsucesssfull" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> UserLogin(UserLoginModel loginModel)
        {
            try
            {
                var user = await userBusiness.UserLogin(loginModel);
                if (user != null)
                {
                    return Ok(new { sucess = true, message = "User Login Sucesssfull", data = user });
                }
                else
                {
                    return BadRequest(new { sucess = false, message = "User Login Unsucesssfull" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordModel forgetPassword)
        {
            try
            {
                var user = await userBusiness.ForgetPassword(forgetPassword);
                if (user != null)
                {
                    return Ok(new { sucess = true, message = "User Forget Password Sucesssfull", Token = user });
                }
                else
                {
                    return BadRequest(new { sucess = false, message = "User Forget Password Unsucesssfull" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPassword)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();

                var user = await userBusiness.ResetPassword(resetPassword, email);
                if (user)
                {
                    return Ok(new { sucess = true, message = "Reset Password Sucesssfull" });
                }
                else
                {
                    return Ok(new { sucess = false, message = "Reset Password Unsucesssfull" });
                }

            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("Users")]
        public async Task<IActionResult> GetAllUserData()
        {
            try
            {
                var userData = await userBusiness.GetAllUserData();
                if (userData != null)
                {
                    return Ok(new { sucess = true, message = "Data Retrive SucessFully", data = userData });
                }
                else
                {
                    return BadRequest(new { sucess = false, message = "Data Retrive Unsucesssfull" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

    }
}
