using BusinessLayer.Interface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Entities;
using System.Collections.Generic;

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
        public IActionResult UserRegister(UserRegistrationModel model)
        {
            try
            {
                var result = userBusiness.UserRegister(model);
                if (result != null)
                {
                    return Ok(new {sucess = true, message = "User Registration Sucesssfull",data = result});
                }
                else
                {
                    return BadRequest(new { sucess = false, message = "User Registration Unsucesssfull"});
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpPost("Login")]
        public IActionResult UserLogin(UserLoginModel loginModel)
        {
            var user = userBusiness.UserLogin(loginModel);
            try
            {
                if (user != null)
                {
                    return Ok(new { sucess = true, message = "User Login Sucesssfull",data = user });
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
        [HttpGet]
        [Route("Users")]
        public IActionResult GetAllUserData()
        {
            try
            {
                var userData = userBusiness.GetAllUserData();
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
