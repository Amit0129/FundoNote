using BusinessLayer.Interface;
using CommonLayer.Models;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundoNote.Controllers
{
    /// <summary>
    /// Collab Controller
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ColabController : ControllerBase
    {
        private readonly ICollaboratorBusiness collaboratorBusiness;
        private readonly IBus _bus;
        public ColabController(ICollaboratorBusiness collaboratorBusiness, IBus bus)
        {
            this.collaboratorBusiness = collaboratorBusiness;
            this._bus = bus;
        }
        /// <summary>
        /// Add collab 
        /// </summary>
        /// <param name="collabModel">New colab Info</param>
        /// <param name="noteId">Note Id</param>
        /// <returns>Collab info</returns>
        [HttpPost]
        [Route("Colab/{noteId}")]
        public async Task<IActionResult> AddCollab(AddCollabModel collabModel, long noteId)
        {
            try
            {
                long userId = long.Parse(User.FindFirst("UserId").Value);
                var colab = await collaboratorBusiness.AddCollab(collabModel, userId, noteId);
                if (colab != null)
                {
                    //Rabbit Mq Publisher

                    //Uri uri = new Uri("rabbitmq://localhost/colabQueue");
                    //var endPoint = await _bus.GetSendEndpoint(uri);
                    //await endPoint.Send(collabModel);
                    SendColabAlartToColabEmail sendMessage = new SendColabAlartToColabEmail();
                    sendMessage.EmailService(colab.CollabEmail);
                    return Ok(new { sucess = true, message = "Colab Email Sucessfull", data = colab });
                }
                else
                {
                    return BadRequest(new { susess = false, message = "Colab Email Failed" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Delete Colab
        /// </summary>
        /// <param name="colabId">Collab Id</param>
        /// <param name="noteId">Note Id</param>
        /// <returns>Boolean Value</returns>
        /// <exception cref="Exception"></exception>
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
                    return Ok(new { sucess = true, message = "Colab Deleted Sucessfull"});
                }
                else
                {
                    return BadRequest(new { susess = false, message = "Colab Delete Failed" });
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Get all colab  of a note
        /// </summary>
        /// <param name="noteId">Note Id</param>
        /// <returns>Gel all collab info of a note</returns>
        /// <exception cref="Exception"></exception>
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
                    return BadRequest(new { sucess = false, message = "Retrive Colab Failed" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
