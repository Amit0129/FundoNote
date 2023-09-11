using BusinessLayer.Interface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepoLayer.Context;
using RepoLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FundoNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteBusiness noteBusiness;
        private readonly IMemoryCache memoryCache;
        private readonly FundoContext fundoContext;
        private readonly IDistributedCache distributedCache;
        public NoteController(INoteBusiness noteBusiness, IMemoryCache memoryCache, FundoContext fundoContext, IDistributedCache distributedCache)
        {
            this.noteBusiness = noteBusiness;
            this.memoryCache = memoryCache;
            this.fundoContext = fundoContext;
            this.distributedCache = distributedCache;
        }
        /// <summary>
        /// Add a new Note
        /// </summary>
        /// <param name="noteModel">New note Info</param>
        /// <returns>Registerd Note info</returns>
        [Authorize]
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
                    return BadRequest(new { sucess = false, message = "Note Added Failed" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Update note
        /// </summary>
        /// <param name="updateNote">update info of note</param>
        /// <returns>Updated note</returns>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateNotes(UpdateNoteModel updateNote)
        {
            try
            {
                var userID = long.Parse(User.FindFirst("UserId").Value);
                var update = await noteBusiness.UpdateNotes(updateNote, userID);
                if (update != null)
                {
                    return Ok(new { sucess = true, message = "Note Updated Sucessfully", data = update });
                }
                else
                {
                    return BadRequest(new { sucess = false, message = "Note Update Failed" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Remove a Note
        /// </summary>
        /// <param name="deleteNote">Note info</param>
        /// <returns>Boolean value</returns>
        [Authorize]
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
                    return BadRequest(new { sucess = false, message = "Note Delete Failed" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Get notes of a perticular user
        /// </summary>
        /// <returns>All notes info of a user</returns>
        [Authorize]
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
                    return BadRequest(new { sucess = false, message = "Retrive All The Note Of User Failed" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize]
        [HttpPatch]
        [Route("IsPin/{noteId}")]
        public async Task<IActionResult> IsPin(long noteId)
        {
            try
            {
                long userId = long.Parse(User.FindFirst("UserId").Value);
                var note = await noteBusiness.IsPin(noteId, userId);
                if (note != null)
                {
                    return Ok(new { sucess = true, messsage = "IsPin Change Sucessfully", data = note });
                }
                else
                {
                    return BadRequest(new { sucess = false, message = "IsPin Change Failed" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize]
        [HttpPatch]
        [Route("IsAchive/{noteId}")]
        public async Task<IActionResult> IsAchive(long noteId)
        {
            try
            {
                long userId = long.Parse(User.FindFirst("UserId").Value);
                var note = await noteBusiness.IsAchive(noteId, userId);
                if (note != null)
                {
                    return Ok(new { suceess = true, message = "Update IsAchive Sucesssfull", data = note });
                }
                else
                {
                    return BadRequest(new { susses = false, message = "IsAchive Update Failed" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize]
        [HttpPatch]
        [Route("IsTrash/{noteId}")]
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
                    return BadRequest(new { sucesss = false, message = "Update IsTrash Failed" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize]
        [HttpPatch]
        [Route("Color/{noteId}/{color}")]
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
                    return BadRequest(new { sucess = false, message = "Update Color Failed" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize]
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
                    return BadRequest(new { sucess = false, message = "Upload Failed" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Get note using Radis cache
        /// </summary>
        /// <returns>All note of user</returns>
        /// <exception cref="Exception"></exception>
        [Authorize]
        [HttpGet("redisGetAllNotes")]
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            try
            {
                var key = "noteList";
                string serializedNoteList;
                var noteList = new List<NotesEntity>();
                var radisNoteList = await distributedCache.GetAsync(key);
                if (radisNoteList != null)
                {
                    serializedNoteList = Encoding.UTF8.GetString(radisNoteList);
                    noteList = JsonConvert.DeserializeObject<List<NotesEntity>>(serializedNoteList);
                }
                else
                {
                    long userId = long.Parse(User.FindFirst("UserId").Value);
                    noteList = (List<NotesEntity>)await noteBusiness.GetUserNotes(userId);
                    serializedNoteList = JsonConvert.SerializeObject(noteList);
                    radisNoteList = Encoding.UTF8.GetBytes(serializedNoteList);
                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                    await distributedCache.SetAsync(key, radisNoteList, options);
                }
                return Ok(new { sucesss = true, message = "Retrive Note Sucessfull", data = noteList });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize]
        [HttpPatch]
        [Route("Reminder/{noteId}/{remin}")]
        public async Task<IActionResult> SetReminder(long noteId, DateTime remin)
        {
            try
            {
                long userId = long.Parse(User.FindFirst("UserId").Value);
                var notes = noteBusiness.SetReminder(userId, noteId, remin);
                if (notes != null)
                {
                    return Ok(new { sucess = true, mssage = "Reminder Set Sucessfull", data = notes });
                }
                else
                {
                    return BadRequest(new { sucess = false, message = "Reminder Set Failed" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize]
        [HttpGet]
        [Route("Search/{serchvalue}")]
        public async Task<IActionResult> SearchQuery(string serchvalue)
        {
            try
            {
                long userId = long.Parse(User.FindFirst("UserId").Value);
                var notes = await noteBusiness.SearchQuery(userId, serchvalue);
                if (notes != null)
                {
                    return Ok(new { sucess = true, message = "Notes Retrive Sucessfull", data = notes });
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
