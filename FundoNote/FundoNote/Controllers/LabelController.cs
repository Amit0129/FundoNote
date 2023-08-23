using BusinessLayer.Interface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Entities;

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
        public IActionResult AddLabel(AddLabelModel addLabel,long noteId)
        {
            try
            {
                long userId = long.Parse(User.FindFirst("UserId").Value);
                var label = labelBusiness.AddLabel(addLabel, userId, noteId);
                if (label!=null)
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
    }
}
