using CourseProject.BLL.Interfaces;
using CourseProject.BLL.Models;
using CourseProject.Core.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CourseProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IndicatorController : ControllerBase
    {
        private readonly IIndicatorService _service;
        private readonly IHubContext<IndicatorHub> _hubContext;

        public IndicatorController(IIndicatorService service, IHubContext<IndicatorHub> hubContext)
        {
            _service = service;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<ActionResult> GetIndicators()
        {
            var indicators = await _service.GetIndicators();

            return Ok(indicators);  
        }

        [HttpPost]
        public async Task<ActionResult> CreateIndicator(IndicatorModel model)
        {
            if(model.Type != IndicatorTypes.Temperature && model.Type != IndicatorTypes.Presence)
            {
                return BadRequest("Invalid indicator type.");
            }

            var id = await _service.CreateIndicator(model);

            return Ok(id);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateIndicatorValue(UpdateIndicatorValueModel model)
        {
            Console.WriteLine($"Received UpdateIndicatorValue request. ID: {model.Id}, Value: {model.Value}");

            var indicators = await _service.GetIndicators();
            var indicator = indicators.FirstOrDefault(i => i.Id == model.Id);

            if (indicator == null)
            {
                return NotFound("Indicator not found.");
            }

            if (indicator.Type == IndicatorTypes.Temperature)
            {
                if(!double.TryParse(model.Value, out _))
                {
                    return BadRequest("Value for temperature indicator must be numeric.");
                }
            }
            else if(indicator.Type == IndicatorTypes.Presence)
            {
                if(model.Value != "Yes" && model.Value != "No")
                {
                    return BadRequest("Value for presence indicator must be 'Yes' or 'No'.");
                }
            }
            Console.WriteLine($"Updating indicator {model.Id} with value {model.Value}.");

            await _service.UpdateIndicatorValue(model);
            await _hubContext.Clients.All.SendAsync("receive", model.Id.ToString(), model.Value);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteIndicator(Guid id)
        {
            await _service.DeleteIndicator(id);

            return NoContent();
        }
    }
}
