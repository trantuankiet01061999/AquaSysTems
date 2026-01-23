using AquaSolution.Server.Services.KPi.KPITaskService;
using AquaSolution.Server.Services.KPi.QuarterCalculateds;
using AquaSolution.Shared.KPI.QuaterCalculated;
using Microsoft.AspNetCore.Mvc;

namespace AquaSolution.Server.Controllers.KPI.QuarterCalculated
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuarterCalculatedController : ControllerBase
    {
        private readonly IQuarterCalculatedService _QuarterCalculatedService;

        public QuarterCalculatedController(IQuarterCalculatedService QuarterCalculatedService)
        {
            _QuarterCalculatedService = QuarterCalculatedService;
        }

        [HttpGet("get-list")]
        public async Task<IActionResult> GetListAsync()
        {
            var list = await _QuarterCalculatedService.LoadListAsync();
            return Ok(list);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody] QuarterCalculatedDto QuarterCalculatedDto)
        {
            var result = await _QuarterCalculatedService.CreatedAsync(QuarterCalculatedDto);
            return result ? Ok(true) : BadRequest("New creation failed");
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] QuarterCalculatedDto QuarterCalculatedDto)
        {
            var result = await _QuarterCalculatedService.UpdateAsync(QuarterCalculatedDto);
            return result ? Ok(true) : BadRequest("Update failed");
        }

        [HttpDelete("delete/{Id}")]
        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            var result = await _QuarterCalculatedService.DeletedAsync(Id);
            return result ? Ok(true) : BadRequest("Delete failed or not found");
        }
    }
}
