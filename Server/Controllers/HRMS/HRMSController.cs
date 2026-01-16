using AquaSolution.Server.Services.HRMS;
using AquaSolution.Shared.HRMS;
using Microsoft.AspNetCore.Mvc;

namespace AquaSolution.Server.Controllers.HRMS
{
    [ApiController]
    [Route("api/[controller]")]
    public class HRMSController : Controller
    {
        private readonly IHRMSService _hrmsService;

        public HRMSController(IHRMSService hrmsService)
        {
            _hrmsService = hrmsService;
        }
        [HttpPost("import-bonus-year")]
        public async Task<IActionResult> ImportBonusYear([FromBody] List<BonusYearDto> data)
        {
            if (data == null || data.Count == 0)
                return BadRequest("Danh sách import rỗng");
            var result = await _hrmsService.ImportExcelAsync(data);
            return Ok(new
            {
                success = result,
                total = data.Count
            });
        }

    }
}
