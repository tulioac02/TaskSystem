using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskSystem.Models;

namespace TaskSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaksController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<TaskModel>> GetAll()
        {
            return Ok();
        }
    }
}
