using BusinessLayer.Interface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Entities;
using System.Collections.Generic;
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
        [Route("AddNote")]
        public IActionResult AddNote(AddNoteModel noteModel)
        {
            try
            {
                long userId = long.Parse(User.FindFirst("UserId").Value);
                var note = noteBusiness.AddNote(noteModel, userId);
                if (note != null)
                {
                    return Ok(new {sucess = true, message = "Note Added Sucessfully", data = note});
                }
                else
                {
                    return BadRequest(new {sucess = false, message = "Note Added Unsucessfully"});
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        //Update Api======================
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateNotes(UpdateNoteModel updateNote)
        {
            try
            {
                var userID = long.Parse(User.FindFirst("UserId").Value);
                var update = noteBusiness.UpdateNotes(updateNote, userID);
                if (update != null)
                {
                    return Ok(new {sucess = true, message = "Note Updated Sucessfully", data = update});
                }
                else
                {
                    return BadRequest(new {sucess = false, message = "Note Updated Unsucessfully"});
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        //Delete  Api=======================
        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteNote(DeleteNoteModel deleteNote)
        {
            long userId = long.Parse(User.FindFirst("UserId").Value);
            try
            {
                var note = noteBusiness.DeleteNote(deleteNote, userId);
                if (note != null)
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
        [Route("GetUserNotes")]
        public IActionResult GetUserNotes()
        {
            try
            {
                long userId = long.Parse(User.FindFirst("UserId").Value);
                var notes = noteBusiness.GetUserNotes(userId);
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
    }
}
