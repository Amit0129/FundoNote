using BusinessLayer.Interface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Entities;
using System.Collections.Generic;

namespace FundoNote.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBusiness labelBusiness;
        public LabelController(ILabelBusiness labelBusiness)
        {
            this.labelBusiness = labelBusiness;
        }
        [HttpPost]
        [Route("AddLabel/{noteId}")]
        public IActionResult AddLabel(AddLabelModel addLabel, long noteId)
        {
            try
            {
                long userId = long.Parse(User.FindFirst("UserId").Value);
                var label = labelBusiness.AddLabel(addLabel, userId, noteId);
                if (label != null)
                {
                    return Ok(new { sucess = true, messaage = "Label Added Sucessfull", data = label });
                }
                else
                {
                    return BadRequest(new { sucess = false, message = "Added Unsucessful" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpGet]
        public IActionResult GetLabels()
        {
            try
            {
                long userId = long.Parse(User.FindFirst("UserId").Value);
                var label = labelBusiness.GetLabels(userId);
                if (label != null)
                {
                    return Ok(new { sucess = true, message = "Retrive Label SucessFull", data = label });
                }
                else
                {
                    return BadRequest(new { sucess = false, message = "Retrive Faild" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpPatch]
        [Route("Update/{labelId}/{labelName}")]
        public IActionResult UpdateLabel(long labelId, string labelName)
        {
            try
            {
                long userId = long.Parse(User.FindFirst("UserId").Value);
                var label = labelBusiness.UpdateLabel(userId,labelId,labelName);
                if (label != null)
                {
                    return Ok(new { sucess = true, message = "Update Sucessfull", data = label });
                }
                return BadRequest(new { sucess = false, message = "Update Failed" });
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("GetLabelById/{noteId}")]
        public IActionResult GetLabelsByNote(long noteId)
        {
            try
            {
                long userId = long.Parse(User.FindFirst("UserId").Value);
                var label = labelBusiness.GetLabelsByNote(userId, noteId);
                if (label != null)
                {
                    return Ok(new { sucess = true, message = "Retrive Label SucessFull", data = label });
                }
                else
                {
                    return BadRequest(new { sucess = false, message = "Retrive Faild" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpDelete]
        [Route("DeleteLabel/{labelId}")]
        public IActionResult DeleteLabel(long labelId)
        {
            try
            {
                long userId = long.Parse(User.FindFirst("UserId").Value);
                var label = labelBusiness.DeleteLabel(userId,labelId);
                if (label)
                {
                    return Ok(new { sucess = true, message = "Delete Label SucessFull" });
                }
                else
                {
                    return BadRequest(new { sucess = false, message = "Delete Faild" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
