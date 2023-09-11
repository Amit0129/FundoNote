using BusinessLayer.Interface;
using CommonLayer.Models;
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
    /// Label controller
    /// </summary>
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
        /// <summary>
        /// Add a new label to a note
        /// </summary>
        /// <param name="addLabel">New label info</param>
        /// <param name="noteId">Note Id</param>
        /// <returns>Register label info</returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        [Route("AddLabel/{noteId}")]
        public async Task<IActionResult> AddLabel(AddLabelModel addLabel, long noteId)
        {
            try
            {
                long userId = long.Parse(User.FindFirst("UserId").Value);
                var label = await labelBusiness.AddLabel(addLabel, userId, noteId);
                if (label != null)
                {
                    return Ok(new { sucess = true, messaage = "Label Added Sucessfull", data = label });
                }
                else
                {
                    return BadRequest(new { sucess = false, message = "Added Failed" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Update Label
        /// </summary>
        /// <param name="labelId">Label Id</param>
        /// <param name="labelName">New Label Name</param>
        /// <returns>updated label info</returns>
        /// <exception cref="Exception"></exception>
        [HttpPatch]
        [Route("Update/{labelId}/{labelName}")]
        public async Task<IActionResult> UpdateLabel(long labelId, string labelName)
        {
            try
            {
                long userId = long.Parse(User.FindFirst("UserId").Value);
                var label = await labelBusiness.UpdateLabel(userId, labelId, labelName);
                if (label != null)
                {
                    return Ok(new { sucess = true, message = "Update Sucessfull", data = label });
                }
                return BadRequest(new { sucess = false, message = "Update Failed" });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Delete Label
        /// </summary>
        /// <param name="labelId">Label Id</param>
        /// <returns>boolean value</returns>
        /// <exception cref="Exception"></exception>
        [HttpDelete]
        [Route("DeleteLabel/{labelId}")]
        public async Task<IActionResult> DeleteLabel(long labelId)
        {
            try
            {
                long userId = long.Parse(User.FindFirst("UserId").Value);
                var label = await labelBusiness.DeleteLabel(userId, labelId);
                if (label)
                {
                    return Ok(new { sucess = true, message = "Delete Label SucessFull" });
                }
                else
                {
                    return BadRequest(new { sucess = false, message = "Delete Failed" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Gel all labels
        /// </summary>
        /// <returns>Get all Label Info</returns>
        [HttpGet]
        public async Task<IActionResult> GetLabels()
        {
            try
            {
                long userId = long.Parse(User.FindFirst("UserId").Value);
                var label = await labelBusiness.GetLabels(userId);
                if (label != null)
                {
                    return Ok(new { sucess = true, message = "Retrive Label SucessFull", data = label });
                }
                else
                {
                    return BadRequest(new { sucess = false, message = "Retrive Failed" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Get label info of a Note
        /// </summary>
        /// <param name="noteId">Note Id</param>
        /// <returns>Labels info of a note</returns>
        [HttpGet]
        [Route("GetLabelById/{noteId}")]
        public async Task<IActionResult> GetLabelsByNote(long noteId)
        {
            try
            {
                long userId = long.Parse(User.FindFirst("UserId").Value);
                var label = await labelBusiness.GetLabelsByNote(userId, noteId);
                if (label != null)
                {
                    return Ok(new { sucess = true, message = "Retrive Label SucessFull", data = label });
                }
                else
                {
                    return BadRequest(new { sucess = false, message = "Retrive Failed" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
