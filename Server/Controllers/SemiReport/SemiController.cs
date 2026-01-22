using AquaSolution.Server.Services.Administration.UserService;
using AquaSolution.Server.Services.ePAD;
using AquaSolution.Server.Services.SemiReport;
using AquaSolution.Server.SignalR;
using AquaSolution.Shared.SemiReport;
using AquaSolution.Shared.UserManagements;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AquaSolution.Server.Controllers.SemiReport
{
    [ApiController]
    [Route("api/[controller]")]
    public class SemiController : ControllerBase
    { private readonly ISemiService _semiService;
        public SemiController(ISemiService semiService)
        {
            _semiService = semiService;
        }
        [HttpGet("get-all-semi-data")]
        public async Task<List<SemiReportDto?>> GetAll()
        {
            var data = await _semiService.GetAllAsync();

            return data;
        }
    }
}
