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
        //Add Note Api=========================
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
                    return BadRequest(new { sucess = false, message = "Note Added Unsucessfully" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        //Update Api======================
        [Authorize]
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
                    return BadRequest(new { sucess = false, message = "Note Deleted Unsucessfully" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        //Get Notes of a User======================
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
                    return BadRequest(new { sucess = false, message = "Retrive All The Note Of User" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        //Api For IsPin==============================
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        //Image Changing Using Cloudinary Api
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
                    return BadRequest(new { sucess = false, message = "Upload Faild" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        //Get All Data Using Radis
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
                    await distributedCache.SetAsync(key,radisNoteList,options);
                }
                return Ok(new { sucesss = true, message = "Image Upload Sucessfull", data = noteList });
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        //Search Query Api
        [Authorize]
        [HttpGet]
        [Route("Search/{serchvalue}")]
        public async Task<IActionResult> SearchQuery(string serchvalue)
        {
            try
            {
                long userId = long.Parse(User.FindFirst("UserId").Value);
                var notes =await noteBusiness.SearchQuery(userId, serchvalue);
                if (notes != null)
                {
                    return Ok(new {sucess = true, message = "Notes Retrive Sucessfull",data = notes});
                }
                else
                {
                    return BadRequest(new {sucess = false, message = "Retrive Faild"});
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
