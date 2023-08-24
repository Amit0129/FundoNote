using BusinessLayer.Interface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FundoNote.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteBusiness noteBusiness;
        public NoteController(INoteBusiness noteBusiness)
        {
            this.noteBusiness = noteBusiness;
        }
        //Add Note Api=========================
        [HttpPost]
        public async Task<IActionResult> AddNote(AddNoteModel noteModel)
        {
            try
            {
                long userId = long.Parse(User.FindFirst("UserId").Value);
                var note = await noteBusiness.AddNote(noteModel, userId);
                if (note != null)
                {
                    return Ok(new { sucess = true, message = "Note Added Sucessfully", data = note });
                }
                else
                {
                    return BadRequest(new { sucess = false, message = "Note Added Unsucessfully" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        //Update Api======================
        [HttpPut]
        public async Task<IActionResult> UpdateNotes(UpdateNoteModel updateNote)
        {
            try
            {
                var userID = long.Parse(User.FindFirst("UserId").Value);
                var update =await noteBusiness.UpdateNotes(updateNote, userID);
                if (update != null)
                {
                    return Ok(new { sucess = true, message = "Note Updated Sucessfully", data = update });
                }
                else
                {
                    return BadRequest(new { sucess = false, message = "Note Updated Unsucessfully" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        //Delete  Api=======================
        [HttpDelete]
        public async Task<IActionResult> DeleteNote(DeleteNoteModel deleteNote)
        {
            long userId = long.Parse(User.FindFirst("UserId").Value);
            try
            {
                var note = await noteBusiness.DeleteNote(deleteNote, userId);
                if (note)
                {
                    return Ok(new { sucess = true, message = "Note Deleted Sucessfully" });


                }
                else
                {
                    return BadRequest(new { sucess = false, message = "Note Deleted Unsucessfully" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        //Get Notes of a User======================
        [HttpGet]
        public async Task<IActionResult> GetUserNotes()
        {
            try
            {
                long userId = long.Parse(User.FindFirst("UserId").Value);
                var notes = await noteBusiness.GetUserNotes(userId);
                if (notes != null)
                {
                    return Ok(new { sucess = true, message = "Retrive All The Note Of User", data = notes });
                }
                else
                {
                    return BadRequest(new { sucess = false, message = "Retrive All The Note Of User" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        //Api For IsPin==============================
        [HttpPatch]
        [Route("IsPin")]
        public async Task<IActionResult> IsPin(long noteId)
        {
            long userId = long.Parse(User.FindFirst("UserId").Value);
            var note =await noteBusiness.IsPin(noteId, userId);
            if (note != null)
            {
                return Ok(new { sucess = true, messsage = "IsPin Change Sucessfully", data = note });
            }
            else
            {
                return BadRequest(new { sucess = false, message = "IsPin Change Unsucessful" });
            }
        }
        //IsAchive Api=================
        [HttpPatch]
        [Route("IsAchive")]
        public async Task<IActionResult> IsAchive(long noteId)
        {
            try
            {
                long userId = long.Parse(User.FindFirst("UserId").Value);
                var note = await noteBusiness.IsAchive(noteId, userId);
                if (note != null)
                {
                    return Ok(new { suceess = true, message = "Update IsTrash Sucesssfull", data = note });
                }
                else
                {
                    return BadRequest(new { susses = false, message = "Update Unsucessfull" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        //IsTrash Api================
        [HttpPatch]
        [Route("IsTrash")]
        public async Task<IActionResult> IsTrash(long noteId)
        {
            try
            {
                long userId = long.Parse(User.FindFirst("UserId").Value);
                var note = await noteBusiness.IsTrash(noteId, userId);
                if (note != null)
                {
                    return Ok(new { sucess = true, message = "Update IsTrash Sucessfull", data = note });
                }
                else
                {
                    return BadRequest(new { sucesss = false, message = "Update Unsucesfull" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        //Color Chnaging
        [HttpPatch]
        [Route("Color")]
        public async Task<IActionResult> Color(long noteId, string color)
        {
            try
            {
                long userId = long.Parse(User.FindFirst("UserId").Value);
                var note = await noteBusiness.Color(noteId, color, userId);
                if (note != null)
                {
                    return Ok(new { sucess = true, message = "Update Color Sucessfull", data = note });
                }
                else
                {
                    return BadRequest(new { sucess = false, message = "Update Unsucessfull" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("Image")]
        public async Task<IActionResult> UploadImage(long noteid, IFormFile img)
        {
            try
            {
                long userId = long.Parse(User.FindFirst("UserId").Value);
                var note = await noteBusiness.UploadImage(noteid, img, userId);
                if (note != null)
                {
                    return Ok(new { sucesss = true, message = "Image Upload Sucessfull", data = note });
                }
                else
                {
                    return BadRequest(new { sucess = false, message = "Upload Faild" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
