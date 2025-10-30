using AquaSolution.Server.Services.KPi.FormulaService;
using Microsoft.AspNetCore.Mvc;

namespace AquaSolution.Server.Controllers.KPI.DealineManagement
{
    [ApiController]
    [Route("api/[controller]")]
    public class DealineManagementController : ControllerBase
    {
        private readonly IDealineManagementService _kPISubmitService;

        public DealineManagementController(IDealineManagementService kPISubmitService)
        {
            _kPISubmitService = kPISubmitService;
        }
        [HttpGet("get-deadline/{userId}")]
        public async Task<IActionResult> GetAsync(Guid userId)
        {
            var result = await _kPISubmitService.GetDealineManagement(userId);
            return Ok(result);
        }
    }
}
