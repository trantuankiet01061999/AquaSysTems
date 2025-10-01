using AquaSolution.Server.Services.ManageMedicalRooms.InventoriesService;
using AquaSolution.Shared.ManageMedicalRooms.Inventories;
using Microsoft.AspNetCore.Mvc;

namespace AquaSolution.Server.Controllers.ManageMedicalRooms.Inventories
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
    
        private readonly IInventoryService _inventoryService;
        public InventoryController(IInventoryService inventoryService) 
        {
            _inventoryService = inventoryService;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _inventoryService.LoadListAsync();
            return Ok(result);
        }
        [HttpGet("get-all-list")]
        public async Task<List<InventoryDto>> GetList()
        {
            var result = await _inventoryService.LoadListAsync();
            return result;
        }
        [HttpGet("get-report")]
        public async Task<IActionResult> GetReportByMonth()
        {
            var result = await _inventoryService.LoadReportInventoryAsync();
            return Ok(result);
        }
        [HttpPost("create-report")]
        public async Task<IActionResult> CreateReport([FromBody] CreatedReportInventoryDto request)
        {
            if (request == null)
                return BadRequest("Invalid data");

            var success = await _inventoryService.InsertReportInventoryAsync(request);

            if (!success)
            {
                // Nếu đã tồn tại báo cáo tháng/năm, trả về lỗi
                return BadRequest($"Report for month {request.Month} and year {request.Year} already exists.");
            }

            return Ok(new { message = "Report created successfully" });
        }
        [HttpGet("get-report/{month:int}/{year:int}")]
        public async Task<IActionResult> GetReportByMonthYear(int month, int year)
        {
            var result = await _inventoryService.LoadReportAsync(month, year);

            if (result == null)
                result = new LoadReportInventoryDto
                {
                    Month = month,
                    Year = year,
                    LoadReportInventoryDetail = new List<LoadReportInventoryDetailDto>()
                };

            return Ok(result);
        }

    }
}
