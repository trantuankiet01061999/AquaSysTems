
using AquaSolution.Server.Services.ScrapManagetment.ReportServices;
using AquaSolution.Server.Services.ScrapManagetment.ScapServices;
using AquaSolution.Server.SignalR;
using AquaSolution.Shared.Enum.Scrap;
using AquaSolution.Shared.ReportDto;
using AquaSolution.Shared.ScrapManagement.Scrap;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;


namespace AquaSolution.Server.Controllers.ScrapManagement
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IExportReportService _exportService;

        public ReportController(IReportService reportService, IExportReportService exportService)
        {
            _reportService = reportService;
            _exportService = exportService;
        }

        // GET api/report/page?FactoryId=...&Period=Month&Year=2026&Month=6
        [HttpGet("page")]
        public async Task<IActionResult> GetPage([FromQuery] ReportFilterDto filter)
        {
            try
            {
                var result = await _reportService.GetReportPageAsync(filter);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary([FromQuery] ReportFilterDto filter)
        {
            var result = await _reportService.GetSummaryAsync(filter);
            return Ok(result);
        }

        [HttpGet("departments")]
        public async Task<IActionResult> GetDepartments([FromQuery] ReportFilterDto filter)
        {
            var result = await _reportService.GetDepartmentReportAsync(filter);
            return Ok(result);
        }

        [HttpGet("materials")]
        public async Task<IActionResult> GetMaterials([FromQuery] ReportFilterDto filter)
        {
            var result = await _reportService.GetMaterialReportAsync(filter);
            return Ok(result);
        }

        [HttpGet("trend")]
        public async Task<IActionResult> GetTrend([FromQuery] ReportFilterDto filter)
        {
            var result = await _reportService.GetTrendAsync(filter);
            return Ok(result);
        }

        [HttpGet("approval-status")]
        public async Task<IActionResult> GetApprovalStatus([FromQuery] ReportFilterDto filter)
        {
            var result = await _reportService.GetApprovalStatusAsync(filter);
            return Ok(result);
        }

        [HttpGet("pipeline")]
        public async Task<IActionResult> GetPipeline([FromQuery] ReportFilterDto filter)
        {
            var result = await _reportService.GetPipelineAsync(filter);
            return Ok(result);
        }
        // ─── Export ──────────────────────────────────────────────────────────
        // GET api/report/export?Period=Month&Year=2026&Month=6&FactoryId=...&FactoryName=Nhà+máy+A
        [HttpGet("export")]
        public async Task<IActionResult> Export([FromQuery] ReportFilterDto filter,
                                                [FromQuery] string factoryName = "Tất cả nhà máy")
        {
            try
            {
                var bytes = await _exportService.ExportAsync(filter, factoryName);

                var periodLabel = filter.Period switch
                {
                    FilterPeriod.Week => $"Tuan{filter.Week}_{filter.Year}",
                    FilterPeriod.Month => $"T{filter.Month}_{filter.Year}",
                    FilterPeriod.Year => $"Nam{filter.Year}",
                    _ => filter.Year.ToString()
                };

                var fileName = $"BaoCaoScrap_{periodLabel}_{DateTime.Now:HHmm}.xlsx";

                return File(
                    bytes,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    fileName
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Lỗi xuất file: {ex.Message}" });
            }
        }
    }
}
