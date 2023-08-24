using BusinessLayer.Interface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundoNote.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ColabController : ControllerBase
    {
        private readonly ICollaboratorBusiness collaboratorBusiness;
        public ColabController(ICollaboratorBusiness collaboratorBusiness)
        {
            this.collaboratorBusiness = collaboratorBusiness;
        }
        [HttpPost]
        [Route("Colab/{noteId}")]
        public async Task<IActionResult> AddCollab(AddCollabModel collabModel, long noteId)
        {
            long userId = long.Parse(User.FindFirst("UserId").Value);
            var colab = await collaboratorBusiness.AddCollab(collabModel, userId, noteId);
            if (colab != null)
            {
                return Ok(new { sucess = true, message = "Colab Email Sucessfull", data = colab });
            }
            else
            {
                return BadRequest(new { susess = false, message = "Colab Email Unsucessfull" });
            }
        }
        [HttpDelete]
        [Route("Delete/{colabId}/{noteId}")]
        public async Task<IActionResult> DeleteAColab(int colabId, long noteId)
        {
            try
            {
                long userId = long.Parse(User.FindFirst("UserId").Value);
                var colab = await collaboratorBusiness.DeleteAColab(colabId, userId, noteId);
                if (colab)
                {
                    return Ok(new { sucess = true, message = "Colab Deleted Sucessfull", data = colab });
                }
                else
                {
                    return BadRequest(new { susess = false, message = "Colab Deleted Unsucessfull" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("GetColab")]
        public async Task<IActionResult> GetAllCollab(long noteId)
        {
            long userID = long.Parse(User.FindFirst("UserId").Value);
            try
            {
                var colab = await collaboratorBusiness.GetAllCollab(userID, noteId);
                if (colab != null)
                {
                    return Ok(new { sucess = true, message = "Retrive Colab Sucessfull", data = colab });
                }
                else
                {
                    return BadRequest(new { sucess = false, message = "Retrive Colab Unsucessfull" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
