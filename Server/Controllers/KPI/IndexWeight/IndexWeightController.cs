
using AquaSolution.Server.Services.KPI.IndexWeight;
using AquaSolution.Shared.KPI.IndexWeight;
using Microsoft.AspNetCore.Mvc;

namespace AquaSolution.Server.Controllers.KPI.IndexWeight
{
    [ApiController]
    [Route("api/[controller]")]
    public class IndexWeightController : ControllerBase
    {
        private readonly IIndexWeightService _IndexWeightService;

        public IndexWeightController(IIndexWeightService IndexWeightService)
        {
            _IndexWeightService = IndexWeightService;
        }

        // GET: api/IndexWeight
        [HttpGet("index-weight")]
        public async Task<IActionResult> GetListAsync()
        {
            var list = await _IndexWeightService.GetListAsync();
            return Ok(list);
        }

        // POST: api/IndexWeight
        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody] IndexWeightDto dto)
        {
            var result = await _IndexWeightService.CreatedAsync(dto);
            return result ? Ok(true) : BadRequest("Creating KPI Task failed.");
        }

        //// PUT: api/IndexWeight
        //[HttpPut("update")]
        //public async Task<IActionResult> UpdateAsync([FromBody] HandleTaskDto dto)
        //{
        //    var result = await _IndexWeightService.UpdateAsync(dto);
        //    return result ? Ok(true) : BadRequest("Update KPI Task failed.");
        //}

        //// DELETE: api/IndexWeight/{id}
        //[HttpDelete("delete/{Id}")]
        //public async Task<IActionResult> DeleteAsync(Guid id)
        //{
        //    var result = await _IndexWeightService.DeletedAsync(id);
        //    return result ? Ok(true) : BadRequest("Delete failed or not found KPI Task.");
        //}
    }
}
