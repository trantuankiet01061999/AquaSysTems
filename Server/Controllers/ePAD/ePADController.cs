using AquaSolution.Server.Services.ePAD;
using AquaSolution.Server.Services.KPi.FormulaService;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
        [HttpGet("get-user-by-workday")]
        public async Task<IActionResult> GetUserByWorkDayId([FromBody] RequestDto req)
        {
            var result = await _ePADService.GetUserByWorkDayId(req.WorkDayId, req.DateTime);
            return new OkObjectResult(result);
        }

        public class RequestDto
        {
            [Required]
            public string WorkDayId { get; set; }
            [Required]
            public string DateTime { get; set; }
        }

    }
}
