using CourseProject.BLL.Interfaces;
using CourseProject.BLL.Models;
using CourseProject.Core.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CourseProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeController : ControllerBase
    {
        private readonly ITimeService _service;

        public TimeController(ITimeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetTime()
        {
            var time = await _service.GetTimeAsync();

            return Ok(new { CurrentTime = time});
        }
    }
}
