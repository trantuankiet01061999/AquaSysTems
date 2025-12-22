using AquaSolution.Server.Services.ePAD;
using AquaSolution.Server.Services.KPi.FormulaService;
using Microsoft.AspNetCore.Mvc;

namespace AquaSolution.Server.Controllers.ePAD
{
    [ApiController]
    [Route("api/[controller]")]
    public class ePADController
    {
        private readonly IePADService _ePADService;

        public ePADController(IePADService ePADService)
        {
            _ePADService = ePADService;
        }
        [HttpGet("get-user-by-workday/{workDayId}")]
        public async Task<IActionResult> GetUserByWorkDayId(string workDayId,string dateTime)
        {
            var result = await _ePADService.GetUserByWorkDayId(workDayId,dateTime);
            return new OkObjectResult(result);
        }
    }
}
