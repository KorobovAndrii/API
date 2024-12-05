using CourseProject.BLL.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace CourseProject.API.Controllers
{
    public class IndicatorHub : Hub
    {
        private readonly IIndicatorService indicatorService;

        public IndicatorHub(IIndicatorService service)
        {
            indicatorService = service;
        }

        public async Task SendValue(string id, string value)
        {
            await indicatorService.UpdateIndicatorValue(new()
            {
                Id = Guid.Parse(id),
                Value = value
            });

            await this.Clients.Others.SendAsync("receive", id, value);
        }
    }
}
