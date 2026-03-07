using AquaSolution.Server.Services.SemiReport.PcbReportService;
using AquaSolution.Server.SignalR;
using AquaSolution.Shared.SemiReport;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AquaSolution.Server.Controllers.SemiReport
{
    [ApiController]
    [Route("api/[controller]")]
    public class PcbReportController : ControllerBase
    {
        private readonly IPcbReportService _pcbService;
        private readonly IHubContext<SignalrHub> _hubContext;
        public PcbReportController(IPcbReportService pcbService, IHubContext<SignalrHub> hubContext)
        {
            _pcbService = pcbService;
            _hubContext = hubContext;
        }
        [HttpGet("get-all-pcb-data")]
        public async Task<List<PcbReportDto?>> GetAll()
        {
            var data = await _pcbService.GetAllAsync();
            await _hubContext.Clients.All.SendAsync("LoadPcbReport");
            return data;
        }
    }
}
