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
    /// <summary>
    /// User controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness userBusiness;
        public UserController(IUserBusiness userBusiness)
        {
            this.userBusiness = userBusiness;
        }
        /// <summary>
        /// User registration
        /// </summary>
        /// <param name="model">New User Info</param>
        /// <returns>Register user info im SMD Format</returns>
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
                    return BadRequest(new { sucess = false, message = "User Registration Failed" });
                }
            }
            catch (System.Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// User Login
        /// </summary>
        /// <param name="loginModel">LogIn Info</param>
        /// <returns>SMD Formart</returns>
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
                    return BadRequest(new { sucess = false, message = "User Login Failed" });
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Forget Password
        /// </summary>
        /// <param name="forgetPassword">forget password model</param>
        /// <returns>token</returns>
        /// <exception cref="Exception"></exception>
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
                    return BadRequest(new { sucess = false, message = "User Forget Password Failed" });
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Reset Password
        /// </summary>
        /// <param name="resetPassword">NewPassword,Confirm Password</param>
        /// <returns>boolean value</returns>
        /// <exception cref="Exception"></exception>
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
                    return BadRequest(new { sucess = false, message = "Reset Password Failed" });
                }

            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
